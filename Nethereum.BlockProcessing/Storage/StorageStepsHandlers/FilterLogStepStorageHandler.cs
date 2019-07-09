using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.Common.Processing;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.BlockchainStore.Repositories.Handlers
{
    public class FilterLogStepStorageHandler : IProcessorHandler<FilterLogVO>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;

        public FilterLogStepStorageHandler(ITransactionLogRepository transactionLogRepository)
        {
            _transactionLogRepository = transactionLogRepository;
        }

        public Task ExecuteAsync(FilterLogVO filterLog)
        {
            return _transactionLogRepository.UpsertAsync(filterLog.Log);
        }
    }
}
