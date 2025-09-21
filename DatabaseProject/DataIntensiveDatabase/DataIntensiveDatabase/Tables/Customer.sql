CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL IDENTITY,
	[CustomerUuid] NVARCHAR(24) PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL 
)
