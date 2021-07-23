CREATE TABLE [dbo].[Todo]
(
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [Description] NVARCHAR(128) NOT NULL, 
    [IsDone] BIT NOT NULL, 
    [CategoryId] UNIQUEIDENTIFIER NOT NULL, 
    [CreationDate] DATETIME2 NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 

    CONSTRAINT [PK_Todo] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Todo_Category] FOREIGN KEY ([CategoryId]) REFERENCES Category([Id]) 
)
