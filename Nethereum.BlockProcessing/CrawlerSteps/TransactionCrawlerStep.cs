using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Nethereum.BlockchainProcessing.Processors.Transactions
{
    public class TransactionCrawlerStep : CrawlerStep<TransactionVO, TransactionVO>
    {
        public TransactionCrawlerStep(IWeb3 web3) : base(web3)
        {
        }

        public override Task<TransactionVO> GetStepDataAsync(TransactionVO parentStep)
        {
            return Task.FromResult(parentStep);
        }
    }
}