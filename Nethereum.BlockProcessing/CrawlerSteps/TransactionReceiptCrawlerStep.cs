using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.Common.Processing;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Nethereum.BlockchainProcessing.Processors.Transactions
{
    public class TransactionReceiptCrawlerStep : CrawlerStep<TransactionVO, TransactionReceiptVO>
    {
        public TransactionReceiptCrawlerStep(IWeb3 web3) : base(web3)
        {
        }

        public override async Task<TransactionReceiptVO> GetStepDataAsync(TransactionVO transactionVO)
        {
            var receipt = await Web3.Eth.Transactions
                .GetTransactionReceipt.SendRequestAsync(transactionVO.Transaction.TransactionHash)
                .ConfigureAwait(false);
            return new TransactionReceiptVO(transactionVO.Block, transactionVO.Transaction, receipt, receipt.HasErrors()?? false);
        }
    }



}