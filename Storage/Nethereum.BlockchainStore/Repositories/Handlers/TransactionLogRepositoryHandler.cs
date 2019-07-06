﻿using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.Handlers;
using Nethereum.BlockProcessing.ValueObjects;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.BlockchainStore.Repositories.Handlers
{
    public class TransactionLogRepositoryHandler : ITransactionLogHandler, ILogHandler
    {
        public TransactionLogRepositoryHandler(ITransactionLogRepository transactionLogRepository)
        {
            TransactionLogRepository = transactionLogRepository;
        }

        public ITransactionLogRepository TransactionLogRepository { get; }

        public async Task HandleAsync(FilterLogVO txLog)
        {
            await TransactionLogRepository.UpsertAsync(
                txLog.Log).ConfigureAwait(false);
        }

        public async Task HandleAsync(FilterLog log)
        {
            await TransactionLogRepository.UpsertAsync(log).ConfigureAwait(false);
        }
    }

}
