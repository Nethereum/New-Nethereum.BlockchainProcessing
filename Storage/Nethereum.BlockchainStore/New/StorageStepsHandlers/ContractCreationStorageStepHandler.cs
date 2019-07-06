using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.Common.Processing;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.BlockchainStore.Repositories.Handlers
{
    public class ContractCreationStorageStepHandler : IProcessorHandler<ContractCreationVO>
    {
        private readonly IContractRepository _contractRepository;
        public ContractCreationStorageStepHandler(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public Task ExecuteAsync(ContractCreationVO contractCreation)
        {
            return _contractRepository.UpsertAsync(
               contractCreation.ContractAddress,
               contractCreation.Code,
               contractCreation.Transaction);
        }
    }
}
