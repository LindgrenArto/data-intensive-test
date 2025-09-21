CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY,
	[UserUuid] NVARCHAR(24) PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Location] NVARCHAR(50) NULL,
	[SiteUuid] NVARCHAR(24) NOT NULL, 

	CONSTRAINT [FK_User_SiteUuid] FOREIGN KEY ([SiteUuid]) REFERENCES [Site]([SiteUuid])
)
