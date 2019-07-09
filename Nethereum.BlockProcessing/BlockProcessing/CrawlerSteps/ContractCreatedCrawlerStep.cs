using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Nethereum.BlockchainProcessing.Processors.Transactions
{
    public class ContractCreatedCrawlerStep: CrawlerStep<TransactionReceiptVO, ContractCreationVO>
    {
        public bool RetrieveCode { get; set; } = false;
        public ContractCreatedCrawlerStep(IWeb3 web3) : base(web3)
        {
        }

        public override async Task<ContractCreationVO> GetStepDataAsync(TransactionReceiptVO transactionReceiptVO)
        {
            var contractAddress = transactionReceiptVO.TransactionReceipt.ContractAddress;
            bool? hasFailed = transactionReceiptVO.TransactionReceipt.HasErrors();
            string code = null;
            if (RetrieveCode && hasFailed != null)
            {
                code = await Web3.Eth.GetCode.SendRequestAsync(contractAddress).ConfigureAwait(false);
                hasFailed = HasFailedToCreateContract(code);
            }
            return new ContractCreationVO(transactionReceiptVO, code,
                hasFailed.Value);
        }

        protected virtual bool HasFailedToCreateContract(string code)
        {
            return (code == null) || (code == "0x");
        }
    }



}