CREATE TABLE [dbo].[SiteUser]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserUuid] NVARCHAR(24) NOT NULL, 
    [SiteUuid] NVARCHAR(24) NOT NULL, 

	CONSTRAINT [FK_User_UserUuid] FOREIGN KEY ([UserUuid]) REFERENCES [USer]([UserUuid]),
	CONSTRAINT [FK_Site_SiteUuid] FOREIGN KEY ([SiteUuid]) REFERENCES [Site]([SiteUuid])
)
