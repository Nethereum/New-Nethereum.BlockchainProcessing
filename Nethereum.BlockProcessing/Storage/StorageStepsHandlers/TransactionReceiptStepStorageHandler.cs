using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.Common.Processing;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;

namespace Nethereum.BlockchainStore.Repositories.Handlers
{
    public class TransactionReceiptStepStorageHandler : IProcessorHandler<TransactionReceiptVO>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAddressTransactionRepository _addressTransactionRepository;

        public TransactionReceiptStepStorageHandler(ITransactionRepository transactionRepository, IAddressTransactionRepository addressTransactionRepository = null)
        {
            _transactionRepository = transactionRepository;
            _addressTransactionRepository = addressTransactionRepository;
        }

        public async Task ExecuteAsync(TransactionReceiptVO transactionReceiptVO)
        {
            await _transactionRepository.UpsertAsync(transactionReceiptVO.Transaction,
                                                     transactionReceiptVO.TransactionReceipt, 
                                                     transactionReceiptVO.Failed, 
                                                     transactionReceiptVO.BlockTimestamp).ConfigureAwait(false);

            if(_addressTransactionRepository != null)
            {
                var newContractAddress = transactionReceiptVO.IsForContractCreation() ? transactionReceiptVO.TransactionReceipt.ContractAddress : string.Empty;
                foreach (var address in transactionReceiptVO.GetAllRelatedAddresses())
                {
                    await _addressTransactionRepository.UpsertAsync(transactionReceiptVO.Transaction,
                                                                    transactionReceiptVO.TransactionReceipt, 
                                                                    transactionReceiptVO.Failed, 
                                                                    transactionReceiptVO.BlockTimestamp, 
                                                                    address, 
                                                                    null, 
                                                                    false, newContractAddress).ConfigureAwait(false);
                }
            }
        }
    }
}
