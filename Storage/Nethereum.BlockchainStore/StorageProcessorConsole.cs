﻿//using Common.Logging;
//using Nethereum.BlockchainProcessing.Processing;
//using Nethereum.BlockchainStore.Repositories;
//using Nethereum.Geth;
//using Nethereum.Web3;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;
//using Nethereum.BlockProcessing.Filters;

//namespace Nethereum.BlockchainStore.Processing
//{
//    public class StorageProcessorConsole
//    {
//        public static async Task<int> Execute(
//            IBlockchainStoreRepositoryFactory repositoryFactory, 
//            IBlockProgressRepository blockProgressRepository,
//            BlockchainSourceConfiguration configuration,
//            FilterContainer filterContainer = null,
//            bool useGeth = false,
//            ILog log = null)
//        {
//            IWeb3 web3 = 
//                useGeth 
//                    ? new Web3Geth(configuration.BlockchainUrl) 
//                    : new Web3.Web3(configuration.BlockchainUrl);

//            bool result;
//            Stopwatch stopWatch = Stopwatch.StartNew();

//            log.Info($"Running from block : '{configuration.FromBlock}' to {configuration.ToBlock}");

//            using (var repositoryHandlerContext = new RepositoryHandlerContext(repositoryFactory, blockProgressRepository))
//            {
//                var blockProcessor = BlockProcessorFactory
//                        .Create(
//                            web3, 
//                            repositoryHandlerContext.Handlers, 
//                            filters: filterContainer,
//                            postVm: configuration.PostVm,
//                            processTransactionsInParallel: configuration.ProcessBlockTransactionsInParallel);

//                var storageProcessingStrategy = new StorageProcessingStrategy(
//                    repositoryHandlerContext, blockProgressRepository, blockProcessor)
//                {
//                    MinimumBlockNumber = configuration.MinimumBlockNumber ?? 0,
//                    MinimumBlockConfirmations = configuration.MinimumBlockConfirmations ?? 0
//                };
                
//                var blockchainProcessor = new BlockchainProcessor(storageProcessingStrategy, log);

//                result = await blockchainProcessor.ExecuteAsync(configuration.FromBlock, configuration.ToBlock)
//                    .ConfigureAwait(false);
//            }

//            System.Console.WriteLine("Duration: " + stopWatch.Elapsed);

//            Debug.WriteLine($"Finished With Success: {result}");
//            System.Console.WriteLine("Finished. Success:" + result);
//            System.Console.ReadLine();

//            return result ? 0 : 1;
                
            
//        }
//    }
//}
