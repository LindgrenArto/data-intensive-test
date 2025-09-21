SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    -- Delete in reverse dependency order

    DELETE FROM dbo.SiteUser;       
    DBCC CHECKIDENT ('dbo.SiteUser', RESEED, 0) WITH NO_INFOMSGS;

    DELETE FROM dbo.Measurement;   
    DBCC CHECKIDENT ('dbo.Measurement', RESEED, 0) WITH NO_INFOMSGS;

    DELETE FROM dbo.Device;        
    DBCC CHECKIDENT ('dbo.Device', RESEED, 0) WITH NO_INFOMSGS;

    DELETE FROM dbo.Site;           
    DBCC CHECKIDENT ('dbo.Site', RESEED, 0) WITH NO_INFOMSGS;

    DELETE FROM dbo.[User];        
    DBCC CHECKIDENT ('dbo.[User]', RESEED, 0) WITH NO_INFOMSGS;

    DELETE FROM dbo.Customer;      
    DBCC CHECKIDENT ('dbo.Customer', RESEED, 0) WITH NO_INFOMSGS;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRAN;
    THROW;
END CATCH;
