SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    -- Customers
    IF NOT EXISTS (SELECT 1 FROM dbo.Customer)
    BEGIN
        ;WITH src(CustomerUuid, Name, City) AS
        (
            SELECT 'CUST000000000000000001','Customer 1','Helsinki'  UNION ALL
            SELECT 'CUST000000000000000002','Customer 2','Espoo'     UNION ALL
            SELECT 'CUST000000000000000003','Customer 3','Vantaa'    UNION ALL
            SELECT 'CUST000000000000000004','Customer 4','Tampere'   UNION ALL
            SELECT 'CUST000000000000000005','Customer 5','Oulu'      UNION ALL
            SELECT 'CUST000000000000000006','Customer 6','Turku'     UNION ALL
            SELECT 'CUST000000000000000007','Customer 7','Lahti'     UNION ALL
            SELECT 'CUST000000000000000008','Customer 8','Kuopio'    UNION ALL
            SELECT 'CUST000000000000000009','Customer 9','Jyväskylä' UNION ALL
            SELECT 'CUST000000000000000010','Customer 10','Joensuu'
        )
        INSERT dbo.Customer (CustomerUuid, Name, City)
        SELECT * FROM src;
    END

    -- Sites
    IF NOT EXISTS (SELECT 1 FROM dbo.Site)
    BEGIN
        ;WITH src(SiteUuid, Name, City, CustomerUuid) AS
        (
            SELECT 'SITE000000000000000001','Site 1','Helsinki' , 'CUST000000000000000001' UNION ALL
            SELECT 'SITE000000000000000002','Site 2','Espoo'    , 'CUST000000000000000002' UNION ALL
            SELECT 'SITE000000000000000003','Site 3','Vantaa'   , 'CUST000000000000000003' UNION ALL
            SELECT 'SITE000000000000000004','Site 4','Tampere'  , 'CUST000000000000000004' UNION ALL
            SELECT 'SITE000000000000000005','Site 5','Oulu'     , 'CUST000000000000000005' UNION ALL
            SELECT 'SITE000000000000000006','Site 6','Turku'    , 'CUST000000000000000006' UNION ALL
            SELECT 'SITE000000000000000007','Site 7','Lahti'    , 'CUST000000000000000007' UNION ALL
            SELECT 'SITE000000000000000008','Site 8','Kuopio'   , 'CUST000000000000000008' UNION ALL
            SELECT 'SITE000000000000000009','Site 9','Jyväskylä', 'CUST000000000000000009' UNION ALL
            SELECT 'SITE000000000000000010','Site 10','Joensuu' , 'CUST000000000000000010'
        )
        INSERT dbo.Site (SiteUuid, Name, City, CustomerUuid)
        SELECT * FROM src;
    END

    -- Devices
    IF NOT EXISTS (SELECT 1 FROM dbo.Device)
    BEGIN
        ;WITH src(DeviceUuid, Name, Location, SiteUuid) AS
        (
            SELECT 'DEV000000000000000001','Device 1','Floor 1',  'SITE000000000000000001' UNION ALL
            SELECT 'DEV000000000000000002','Device 2','Floor 2',  'SITE000000000000000002' UNION ALL
            SELECT 'DEV000000000000000003','Device 3','Floor 3',  'SITE000000000000000003' UNION ALL
            SELECT 'DEV000000000000000004','Device 4','Floor 4',  'SITE000000000000000004' UNION ALL
            SELECT 'DEV000000000000000005','Device 5','Floor 5',  'SITE000000000000000005' UNION ALL
            SELECT 'DEV000000000000000006','Device 6','Floor 6',  'SITE000000000000000006' UNION ALL
            SELECT 'DEV000000000000000007','Device 7','Floor 7',  'SITE000000000000000007' UNION ALL
            SELECT 'DEV000000000000000008','Device 8','Floor 8',  'SITE000000000000000008' UNION ALL
            SELECT 'DEV000000000000000009','Device 9','Floor 9',  'SITE000000000000000009' UNION ALL
            SELECT 'DEV000000000000000010','Device 10','Floor 10','SITE000000000000000010'
        )
        INSERT dbo.Device (DeviceUuid, Name, Location, SiteUuid)
        SELECT * FROM src;
    END

    -- Users
    IF NOT EXISTS (SELECT 1 FROM dbo.[User])
    BEGIN
        ;WITH src(UserUuid, Name, Location) AS
        (
            SELECT 'USR000000000000000001','User 1','Ops' UNION ALL
            SELECT 'USR000000000000000002','User 2','Ops' UNION ALL
            SELECT 'USR000000000000000003','User 3','Ops' UNION ALL
            SELECT 'USR000000000000000004','User 4','Ops' UNION ALL
            SELECT 'USR000000000000000005','User 5','Ops' UNION ALL
            SELECT 'USR000000000000000006','User 6','Ops' UNION ALL
            SELECT 'USR000000000000000007','User 7','Ops' UNION ALL
            SELECT 'USR000000000000000008','User 8','Ops' UNION ALL
            SELECT 'USR000000000000000009','User 9','Ops' UNION ALL
            SELECT 'USR000000000000000010','User 10','Ops'
        )
        INSERT dbo.[User] (UserUuid, Name, Location)
        SELECT * FROM src;
    END

    -- Measurements
    IF NOT EXISTS (SELECT 1 FROM dbo.Measurement)
    BEGIN
        ;WITH src(MeasurementUuid, Name, Location, DeviceUuid, Measurement) AS
        (
            SELECT 'MEA000000000000000001','Sensor 1','Room 1','DEV000000000000000001',20.5 UNION ALL
            SELECT 'MEA000000000000000002','Sensor 2','Room 2','DEV000000000000000002',21.1 UNION ALL
            SELECT 'MEA000000000000000003','Sensor 3','Room 3','DEV000000000000000003',19.8 UNION ALL
            SELECT 'MEA000000000000000004','Sensor 4','Room 4','DEV000000000000000004',22.2 UNION ALL
            SELECT 'MEA000000000000000005','Sensor 5','Room 5','DEV000000000000000005',23.0 UNION ALL
            SELECT 'MEA000000000000000006','Sensor 6','Room 6','DEV000000000000000006',18.9 UNION ALL
            SELECT 'MEA000000000000000007','Sensor 7','Room 7','DEV000000000000000007',21.7 UNION ALL
            SELECT 'MEA000000000000000008','Sensor 8','Room 8','DEV000000000000000008',20.9 UNION ALL
            SELECT 'MEA000000000000000009','Sensor 9','Room 9','DEV000000000000000009',19.6 UNION ALL
            SELECT 'MEA000000000000000010','Sensor 10','Room 10','DEV000000000000000010',22.0
        )
        INSERT dbo.Measurement (MeasurementUuid, Name, Location, DeviceUuid, Measurement)
        SELECT * FROM src;
    END

    -- SiteUser links
    IF NOT EXISTS (SELECT 1 FROM dbo.SiteUser)
    BEGIN
        ;WITH u AS (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS rn, UserUuid FROM dbo.[User]),
              s AS (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS rn, SiteUuid FROM dbo.Site),
              pairs AS
        (
            SELECT u.UserUuid, s1.SiteUuid
            FROM u JOIN s s1 ON s1.rn = u.rn
            UNION ALL
            SELECT u.UserUuid, s2.SiteUuid
            FROM u JOIN s s2 ON s2.rn = CASE WHEN u.rn=10 THEN 1 ELSE u.rn+1 END
        )
        INSERT dbo.SiteUser (UserUuid, SiteUuid)
        SELECT UserUuid, SiteUuid FROM pairs;
    END

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRAN;
    THROW;
END CATCH;
