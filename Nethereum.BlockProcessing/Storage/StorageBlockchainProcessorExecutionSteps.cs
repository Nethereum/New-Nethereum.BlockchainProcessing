﻿using Nethereum.BlockchainProcessing.Common.Processing;
using Nethereum.BlockchainStore.Repositories.Handlers;

namespace Nethereum.BlockchainStore.Repositories
{
    public class StorageBlockchainProcessorExecutionSteps: BlockchainProcessorExecutionSteps
    {
        public StorageBlockchainProcessorExecutionSteps(IBlockchainStoreRepositoryFactory repositoryFactory)
        {
            AddBlockStepStorageHandler(repositoryFactory);
            AddContractCreationStepStorageHandler(repositoryFactory);
            AddTransactionReceiptStepStorageHandler(repositoryFactory);
            AddFilterLogStepStorageHandler(repositoryFactory);
        }

        protected virtual void AddBlockStepStorageHandler(IBlockchainStoreRepositoryFactory repositoryFactory)
        {
            var handler = new BlockStepStorageHandler(repositoryFactory.CreateBlockRepository());
            this.BlockStepProcessor.AddProcessorHandler(handler);
        }

        protected virtual void AddContractCreationStepStorageHandler(IBlockchainStoreRepositoryFactory repositoryFactory)
        {
            var handler = new ContractCreationStorageStepHandler(repositoryFactory.CreateContractRepository());
            this.ContractCreationStepProcessor.AddProcessorHandler(handler);
        }

        protected virtual void AddTransactionReceiptStepStorageHandler(IBlockchainStoreRepositoryFactory repositoryFactory)
        {
            var handler = new TransactionReceiptStepStorageHandler(repositoryFactory.CreateTransactionRepository(), repositoryFactory.CreateAddressTransactionRepository());
            this.TransactionReceiptStepProcessor.AddProcessorHandler(handler);
        }

        protected virtual void AddFilterLogStepStorageHandler(IBlockchainStoreRepositoryFactory repositoryFactory)
        {
            var handler = new FilterLogStepStorageHandler(repositoryFactory.CreateTransactionLogRepository());
            this.FilterLogStepProcesor.AddProcessorHandler(handler);
        }
    }
}
