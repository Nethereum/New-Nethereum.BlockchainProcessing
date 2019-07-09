﻿using Nethereum.RPC.Eth.DTOs;
using System.Linq;
using Nethereum.Contracts;

namespace Nethereum.BlockchainStore.Entities.Mapping
{
    public static class TransactionLogMapping
    {
        public static void Map(this TransactionLog transactionLog, FilterLog log)
        {
            transactionLog.TransactionHash = log.TransactionHash;
            transactionLog.LogIndex = log.LogIndex.ToLong();
            transactionLog.Address = log.Address;
            transactionLog.Data = log.Data;

            transactionLog.EventHash = log.EventSignature();
            transactionLog.IndexVal1 = log.IndexedVal1();
            transactionLog.IndexVal2 = log.IndexedVal2();
            transactionLog.IndexVal3 = log.IndexedVal3();
        }

        /// <summary>
        /// Create a partially populated FilterLog from the data stored in the view
        /// The view does not contain all fields in 
        /// </summary>
        public static FilterLog ToFilterLog(this ITransactionLogView transactionLogView)
        {
            return new FilterLog
            {
                TransactionHash = transactionLogView.TransactionHash,
                Address = transactionLogView.Address,
                Data = transactionLogView.Data,
                LogIndex = new Hex.HexTypes.HexBigInteger(transactionLogView.LogIndex),
                Topics = new[] {transactionLogView.EventHash, 
                                transactionLogView.IndexVal1, 
                                transactionLogView.IndexVal2, 
                                transactionLogView.IndexVal3}
                    .Where(s =>! string.IsNullOrEmpty(s)).ToArray()
            };
        }
    }
}
