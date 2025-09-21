CREATE TABLE [dbo].[Device]
(
	[Id] INT NOT NULL IDENTITY,
	[DeviceUuid] NVARCHAR(24) PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Location] NVARCHAR(50) NULL,
	[SiteUuid] NVARCHAR(24) NOT NULL, 

	CONSTRAINT [FK_Device_SiteUuid] FOREIGN KEY ([SiteUuid]) REFERENCES [Site]([SiteUuid])
)
