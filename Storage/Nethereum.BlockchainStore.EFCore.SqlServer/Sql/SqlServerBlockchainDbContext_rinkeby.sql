﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'rinkeby') IS NULL EXEC(N'CREATE SCHEMA [rinkeby];');

GO

CREATE TABLE [rinkeby].[AddressTransactions] (
    [RowIndex] int NOT NULL IDENTITY,
    [RowCreated] datetime2 NULL,
    [RowUpdated] datetime2 NULL,
    [BlockNumber] nvarchar(100) NOT NULL,
    [Hash] nvarchar(67) NOT NULL,
    [Address] nvarchar(43) NOT NULL,
    CONSTRAINT [PK_AddressTransactions] PRIMARY KEY ([RowIndex])
);

GO

CREATE TABLE [rinkeby].[Blocks] (
    [RowIndex] int NOT NULL IDENTITY,
    [RowCreated] datetime2 NULL,
    [RowUpdated] datetime2 NULL,
    [BlockNumber] nvarchar(43) NOT NULL,
    [Hash] nvarchar(67) NOT NULL,
    [ParentHash] nvarchar(67) NOT NULL,
    [Nonce] bigint NOT NULL,
    [ExtraData] nvarchar(max) NULL,
    [Difficulty] bigint NOT NULL,
    [TotalDifficulty] bigint NOT NULL,
    [Size] bigint NOT NULL,
    [Miner] nvarchar(43) NULL,
    [GasLimit] bigint NOT NULL,
    [GasUsed] bigint NOT NULL,
    [Timestamp] bigint NOT NULL,
    [TransactionCount] bigint NOT NULL,
    CONSTRAINT [PK_Blocks] PRIMARY KEY ([RowIndex])
);

GO

CREATE TABLE [rinkeby].[Contracts] (
    [RowIndex] int NOT NULL IDENTITY,
    [RowCreated] datetime2 NULL,
    [RowUpdated] datetime2 NULL,
    [Address] nvarchar(43) NULL,
    [Name] nvarchar(255) NULL,
    [ABI] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    [Creator] nvarchar(43) NULL,
    [TransactionHash] nvarchar(67) NULL,
    CONSTRAINT [PK_Contracts] PRIMARY KEY ([RowIndex])
);

GO

CREATE TABLE [rinkeby].[TransactionLogs] (
    [RowIndex] int NOT NULL IDENTITY,
    [RowCreated] datetime2 NULL,
    [RowUpdated] datetime2 NULL,
    [TransactionHash] nvarchar(450) NOT NULL,
    [LogIndex] bigint NOT NULL,
    [Address] nvarchar(43) NULL,
    [EventHash] nvarchar(67) NULL,
    [IndexVal1] nvarchar(67) NULL,
    [IndexVal2] nvarchar(67) NULL,
    [IndexVal3] nvarchar(67) NULL,
    [Data] nvarchar(max) NULL,
    CONSTRAINT [PK_TransactionLogs] PRIMARY KEY ([RowIndex])
);

GO

CREATE TABLE [rinkeby].[TransactionLogVmStacks] (
    [RowIndex] int NOT NULL IDENTITY,
    [RowCreated] datetime2 NULL,
    [RowUpdated] datetime2 NULL,
    [Address] nvarchar(43) NULL,
    [TransactionHash] nvarchar(67) NULL,
    [StructLogs] nvarchar(max) NULL,
    CONSTRAINT [PK_TransactionLogVmStacks] PRIMARY KEY ([RowIndex])
);

GO

CREATE TABLE [rinkeby].[Transactions] (
    [RowIndex] int NOT NULL IDENTITY,
    [RowCreated] datetime2 NULL,
    [RowUpdated] datetime2 NULL,
    [BlockHash] nvarchar(67) NULL,
    [BlockNumber] nvarchar(100) NOT NULL,
    [Hash] nvarchar(67) NOT NULL,
    [AddressFrom] nvarchar(43) NULL,
    [TimeStamp] bigint NOT NULL,
    [TransactionIndex] bigint NOT NULL,
    [Value] nvarchar(100) NULL,
    [AddressTo] nvarchar(43) NULL,
    [Gas] bigint NOT NULL,
    [GasPrice] bigint NOT NULL,
    [Input] nvarchar(max) NULL,
    [Nonce] bigint NOT NULL,
    [Failed] bit NOT NULL,
    [ReceiptHash] nvarchar(67) NULL,
    [GasUsed] bigint NOT NULL,
    [CumulativeGasUsed] bigint NOT NULL,
    [HasLog] bit NOT NULL,
    [Error] nvarchar(max) NULL,
    [HasVmStack] bit NOT NULL,
    [NewContractAddress] nvarchar(43) NULL,
    [FailedCreateContract] bit NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([RowIndex])
);

GO

CREATE INDEX [IX_AddressTransactions_Address] ON [rinkeby].[AddressTransactions] ([Address]);

GO

CREATE INDEX [IX_AddressTransactions_Hash] ON [rinkeby].[AddressTransactions] ([Hash]);

GO

CREATE UNIQUE INDEX [IX_AddressTransactions_BlockNumber_Hash_Address] ON [rinkeby].[AddressTransactions] ([BlockNumber], [Hash], [Address]);

GO

CREATE UNIQUE INDEX [IX_Blocks_BlockNumber_Hash] ON [rinkeby].[Blocks] ([BlockNumber], [Hash]);

GO

CREATE INDEX [IX_Contracts_Address] ON [rinkeby].[Contracts] ([Address]);

GO

CREATE INDEX [IX_Contracts_Name] ON [rinkeby].[Contracts] ([Name]);

GO

CREATE INDEX [IX_TransactionLogs_Address] ON [rinkeby].[TransactionLogs] ([Address]);

GO

CREATE INDEX [IX_TransactionLogs_EventHash] ON [rinkeby].[TransactionLogs] ([EventHash]);

GO

CREATE INDEX [IX_TransactionLogs_IndexVal1] ON [rinkeby].[TransactionLogs] ([IndexVal1]);

GO

CREATE INDEX [IX_TransactionLogs_IndexVal2] ON [rinkeby].[TransactionLogs] ([IndexVal2]);

GO

CREATE INDEX [IX_TransactionLogs_IndexVal3] ON [rinkeby].[TransactionLogs] ([IndexVal3]);

GO

CREATE UNIQUE INDEX [IX_TransactionLogs_TransactionHash_LogIndex] ON [rinkeby].[TransactionLogs] ([TransactionHash], [LogIndex]);

GO

CREATE INDEX [IX_TransactionLogVmStacks_Address] ON [rinkeby].[TransactionLogVmStacks] ([Address]);

GO

CREATE INDEX [IX_TransactionLogVmStacks_TransactionHash] ON [rinkeby].[TransactionLogVmStacks] ([TransactionHash]);

GO

CREATE INDEX [IX_Transactions_AddressFrom] ON [rinkeby].[Transactions] ([AddressFrom]);

GO

CREATE INDEX [IX_Transactions_AddressTo] ON [rinkeby].[Transactions] ([AddressTo]);

GO

CREATE INDEX [IX_Transactions_Hash] ON [rinkeby].[Transactions] ([Hash]);

GO

CREATE INDEX [IX_Transactions_NewContractAddress] ON [rinkeby].[Transactions] ([NewContractAddress]);

GO

CREATE UNIQUE INDEX [IX_Transactions_BlockNumber_Hash] ON [rinkeby].[Transactions] ([BlockNumber], [Hash]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181010105740_InitialCreate', N'2.1.1-rtm-30846');

GO

