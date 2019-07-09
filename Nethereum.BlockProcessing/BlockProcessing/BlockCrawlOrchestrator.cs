using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.Common.Processing;
using Nethereum.BlockchainProcessing.Processors.Transactions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System.Linq;

namespace Nethereum.BlockchainProcessing.Processors
{

    // var x = web3.Eth.Processing.CreateBlockProcessor(Steps);
    // x = web3.Eth.Processing.CreateStorageBlockProcessor(AzureAdapter, AzureProgress)
    // x.Steps.BlockStep.AddHandler(
    //
    // var processing = new Processing(1, 100)
    // processing.TransactionStep.SetStepMatchingCriteria(x = > x == true);
    // processing.TransactionStep.AddHandler(x = > Save(x), IsForMe()); 


    public class BlockCrawlOrchestrator: IBlockchainProcessingOrchestrator
    {
        protected IWeb3 Web3 { get; set; }
        public IEnumerable<BlockchainProcessorExecutionSteps> ExecutionStepsCollection { get; }
        protected BlockCrawlerStep BlockCrawlerStep { get; }
        protected TransactionCrawlerStep TransactionWithBlockCrawlerStep { get; }
        protected TransactionReceiptCrawlerStep TransactionWithReceiptCrawlerStep { get; }
        protected ContractCreatedCrawlerStep ContractCreatedCrawlerStep { get; }

        public BlockCrawlOrchestrator(IWeb3 web3, IEnumerable<BlockchainProcessorExecutionSteps> executionStepsCollection)
        {
            
            this.ExecutionStepsCollection = executionStepsCollection;
            Web3 = web3;
            BlockCrawlerStep = new BlockCrawlerStep(web3);
            TransactionWithBlockCrawlerStep = new TransactionCrawlerStep(web3);
            TransactionWithReceiptCrawlerStep = new TransactionReceiptCrawlerStep(web3);
            ContractCreatedCrawlerStep = new ContractCreatedCrawlerStep(web3);
        }

        public virtual async Task CrawlBlock(BigInteger blockNumber)
        {
            var blockCrawlerStepCompleted = await BlockCrawlerStep.ExecuteStepAsync(blockNumber, ExecutionStepsCollection);
            await CrawlTransactions(blockCrawlerStepCompleted);

        }
        protected virtual async Task CrawlTransactions(CrawlerStepCompleted<BlockWithTransactions> completedStep)
        {
            if (completedStep != null)
            {
                foreach (var txn in completedStep.StepData.Transactions)
                {
                    await CrawlTransaction(completedStep, txn);
                }
            }
        }
        protected virtual async Task CrawlTransaction(CrawlerStepCompleted<BlockWithTransactions> completedStep, Transaction txn)
        {
            var currentStepCompleted = await TransactionWithBlockCrawlerStep.ExecuteStepAsync(
                new TransactionVO(txn, completedStep.StepData), completedStep.ExecutedStepsCollection);
            await CrawlTransactionReceipt(currentStepCompleted);
        }

        protected virtual async Task CrawlTransactionReceipt(CrawlerStepCompleted<TransactionVO> completedStep)
        {
           var currentStepCompleted = await TransactionWithReceiptCrawlerStep.ExecuteStepAsync(completedStep.StepData,
                completedStep.ExecutedStepsCollection);
            if(currentStepCompleted != null && currentStepCompleted.StepData.IsForContractCreation())
            {
                await ContractCreatedCrawlerStep.ExecuteStepAsync(currentStepCompleted.StepData, completedStep.ExecutedStepsCollection);
            }
        }

        public async Task<OrchestrationProgress> ProcessAsync(BigInteger fromNumber, BigInteger toNumber)
        {
            var progress = new OrchestrationProgress();
            try
            {
                var currentBlockNumber = fromNumber;
                while (currentBlockNumber <= toNumber)
                {

                    await CrawlBlock(currentBlockNumber);
                    progress.BlockNumberProcessTo = currentBlockNumber;
                    currentBlockNumber = currentBlockNumber + 1;
                }
            }
            catch (Exception ex)
            {
                progress.Exception = ex;
            }

            return progress;
        }
    }
}