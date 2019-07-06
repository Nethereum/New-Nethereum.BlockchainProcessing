﻿using Nethereum.BlockchainProcessing.Handlers;
using Nethereum.BlockProcessing.ValueObjects;
using Nethereum.Contracts;

namespace Nethereum.BlockchainStore.Search
{
    public struct FunctionCall<TDto> where TDto : FunctionMessage, new()
    {
        public FunctionCall(TransactionReceiptVO tx, TDto dto)
        {
            Tx = tx;
            Dto = dto;
        }

        public TransactionReceiptVO Tx { get; }
        public TDto Dto { get; }
    }
}
