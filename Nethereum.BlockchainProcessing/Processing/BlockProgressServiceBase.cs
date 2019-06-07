﻿using System.Threading.Tasks;

namespace Nethereum.BlockchainProcessing.Processing
{
    public abstract class BlockProgressServiceBase: IBlockProgressService
    {
        protected ulong? DefaultStartingBlockNumber;
        private readonly IBlockProgressRepository _blockProgressRepository;

        protected BlockProgressServiceBase(
            ulong? defaultStartingBlockNumber, 
            IBlockProgressRepository blockProgressRepository)
        {
            DefaultStartingBlockNumber = defaultStartingBlockNumber;
            _blockProgressRepository = blockProgressRepository;
        }

        public virtual async Task SaveLastBlockProcessedAsync(ulong blockNumber)
        {
            await _blockProgressRepository
                .UpsertProgressAsync(blockNumber)
                .ConfigureAwait(false);
        }

        protected virtual Task<ulong> GetMinBlockNumber() => Task.FromResult((ulong)0);

        private async Task<ulong> LazyLoadDefaultStartingBlockNumber()
        {
            if(DefaultStartingBlockNumber == null)
            {
                DefaultStartingBlockNumber = await GetMinBlockNumber().ConfigureAwait(false);
            }

            return DefaultStartingBlockNumber.Value;
        } 

        public virtual async Task<ulong> GetBlockNumberToProcessFrom()
        {
            var lastBlockProcessed = await _blockProgressRepository
                .GetLastBlockNumberProcessedAsync()
                .ConfigureAwait(false);

            if (lastBlockProcessed != null)
            {
                //last block plus one
                return lastBlockProcessed.Value + 1;
            }

            return await LazyLoadDefaultStartingBlockNumber().ConfigureAwait(false);
        }

        public abstract Task<ulong> GetBlockNumberToProcessTo();

        public virtual async Task<BlockRange?> GetNextBlockRangeToProcessAsync(
            uint maxNumberOfBlocksPerBatch)
        {
            var from = await GetBlockNumberToProcessFrom().ConfigureAwait(false);
            var to = await GetBlockNumberToProcessTo().ConfigureAwait(false);

            if (to < from) 
                return null;

            //if we start at say block 10
            //and we only want 1 block processed
            //we should process from block 10 to block 10

            var zeroBasedMaxBlock = maxNumberOfBlocksPerBatch == 0 ? maxNumberOfBlocksPerBatch : maxNumberOfBlocksPerBatch - 1;

            if ((to - from) > zeroBasedMaxBlock)
            {
                to = from + zeroBasedMaxBlock;
            }

            return new BlockRange(from, to);
        }
    }
}
