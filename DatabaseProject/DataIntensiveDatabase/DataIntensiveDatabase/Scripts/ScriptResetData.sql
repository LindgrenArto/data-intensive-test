SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    IF OBJECT_ID('dbo.SiteUser','U') IS NOT NULL
    BEGIN
        DELETE FROM dbo.SiteUser;
        IF COL_LENGTH('dbo.SiteUser','Id') IS NOT NULL
            DBCC CHECKIDENT ('dbo.SiteUser', RESEED, 0) WITH NO_INFOMSGS;
    END

    IF OBJECT_ID('dbo.Measurement','U') IS NOT NULL
    BEGIN
        DELETE FROM dbo.Measurement;
        IF COL_LENGTH('dbo.Measurement','Id') IS NOT NULL
            DBCC CHECKIDENT ('dbo.Measurement', RESEED, 0) WITH NO_INFOMSGS;
    END

    IF OBJECT_ID('dbo.Device','U') IS NOT NULL
    BEGIN
        DELETE FROM dbo.Device;
        IF COL_LENGTH('dbo.Device','Id') IS NOT NULL
            DBCC CHECKIDENT ('dbo.Device', RESEED, 0) WITH NO_INFOMSGS;
    END

    IF OBJECT_ID('dbo.Site','U') IS NOT NULL
    BEGIN
        DELETE FROM dbo.Site;
        IF COL_LENGTH('dbo.Site','Id') IS NOT NULL
            DBCC CHECKIDENT ('dbo.Site', RESEED, 0) WITH NO_INFOMSGS;
    END

    IF OBJECT_ID('dbo.[User]','U') IS NOT NULL
    BEGIN
        DELETE FROM dbo.[User];
        IF COL_LENGTH('dbo.[User]','Id') IS NOT NULL
            DBCC CHECKIDENT ('dbo.[User]', RESEED, 0) WITH NO_INFOMSGS;
    END

    IF OBJECT_ID('dbo.Customer','U') IS NOT NULL
    BEGIN
        DELETE FROM dbo.Customer;
        IF COL_LENGTH('dbo.Customer','Id') IS NOT NULL
            DBCC CHECKIDENT ('dbo.Customer', RESEED, 0) WITH NO_INFOMSGS;
    END

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRAN;
    THROW;
END CATCH;
