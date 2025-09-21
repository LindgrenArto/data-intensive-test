CREATE TABLE [dbo].[Site]
(
	[Id] INT NOT NULL IDENTITY,
	[SiteUuid] NVARCHAR(24) PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL,
	[CustomerUuid] NVARCHAR(24) NOT NULL, 

	CONSTRAINT [FK_Site_CustomerUuid] FOREIGN KEY ([CustomerUuid]) REFERENCES [Customer]([CustomerUuid])
)
