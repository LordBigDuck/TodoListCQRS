CREATE TABLE [dbo].[Category]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [CreationDate] DATETIME2 NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 

    CONSTRAINT [PK_Category] PRIMARY KEY ([Id]) 
)
