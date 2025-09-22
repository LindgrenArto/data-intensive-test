SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    DECLARE @EnvTag nvarchar(16) = '$(EnvTag)';
    IF NULLIF(@EnvTag,'') IS NULL
        SELECT @EnvTag = UPPER(SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA1', DB_NAME())), 3, 4));

    -------------------------
    -- Build temp datasets --
    -------------------------
    IF OBJECT_ID('tempdb..#nums') IS NOT NULL DROP TABLE #nums;
    SELECT n INTO #nums
    FROM (VALUES (1),(2),(3),(4),(5),(6),(7),(8),(9),(10)) v(n);

    -- common 1..5
    IF OBJECT_ID('tempdb..#c_cust') IS NOT NULL DROP TABLE #c_cust;
    SELECT
        CustomerUuid = CONCAT('CUST', REPLICATE('0', 24-4-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Name = CONCAT('Customer ', n),
        City = CHOOSE(n,'Helsinki','Espoo','Vantaa','Tampere','Oulu','Turku','Lahti','Kuopio','Jyväskylä','Joensuu')
    INTO #c_cust
    FROM #nums WHERE n <= 5;

    IF OBJECT_ID('tempdb..#c_site') IS NOT NULL DROP TABLE #c_site;
    SELECT
        SiteUuid = CONCAT('SITE', REPLICATE('0', 24-4-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Name = CONCAT('Site ', n),
        City = CHOOSE(n,'Helsinki','Espoo','Vantaa','Tampere','Oulu','Turku','Lahti','Kuopio','Jyväskylä','Joensuu'),
        CustomerUuid = CONCAT('CUST', REPLICATE('0', 24-4-3), RIGHT('00'+CAST(n AS varchar(3)),3))
    INTO #c_site
    FROM #nums WHERE n <= 5;

    IF OBJECT_ID('tempdb..#c_dev') IS NOT NULL DROP TABLE #c_dev;
    SELECT
        DeviceUuid = CONCAT('DEV', REPLICATE('0', 24-3-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Name = CONCAT('Device ', n),
        Location = CONCAT('Floor ', n),
        SiteUuid = CONCAT('SITE', REPLICATE('0', 24-4-3), RIGHT('00'+CAST(n AS varchar(3)),3))
    INTO #c_dev
    FROM #nums WHERE n <= 5;

    IF OBJECT_ID('tempdb..#c_usr') IS NOT NULL DROP TABLE #c_usr;
    SELECT
        UserUuid = CONCAT('USR', REPLICATE('0', 24-3-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Name = CONCAT('User ', n),
        Location = 'Ops'
    INTO #c_usr
    FROM #nums WHERE n <= 5;

    IF OBJECT_ID('tempdb..#c_mea') IS NOT NULL DROP TABLE #c_mea;
    SELECT
        MeasurementUuid = CONCAT('MEA', REPLICATE('0', 24-3-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Name = CONCAT('Sensor ', n),
        Location = CONCAT('Room ', n),
        DeviceUuid = CONCAT('DEV', REPLICATE('0', 24-3-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Measurement = CAST(20.0 + (n*0.3) AS decimal(4,1))
    INTO #c_mea
    FROM #nums WHERE n <= 5;


    IF OBJECT_ID('tempdb..#e_ids') IS NOT NULL DROP TABLE #e_ids;
    SELECT
        n,
        cust = CONCAT('CUST', @EnvTag, REPLICATE('0', 24 - LEN('CUST') - LEN(@EnvTag) - 3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        site = CONCAT('SITE', @EnvTag, REPLICATE('0', 24 - LEN('SITE') - LEN(@EnvTag) - 3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        dev  = CONCAT('DEV',  @EnvTag, REPLICATE('0', 24 - LEN('DEV')  - LEN(@EnvTag) - 3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        usr  = CONCAT('USR',  @EnvTag, REPLICATE('0', 24 - LEN('USR')  - LEN(@EnvTag) - 3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        mea  = CONCAT('MEA',  @EnvTag, REPLICATE('0', 24 - LEN('MEA')  - LEN(@EnvTag) - 3), RIGHT('00'+CAST(n AS varchar(3)),3))
    INTO #e_ids
    FROM #nums WHERE n BETWEEN 6 AND 10;

    IF OBJECT_ID('tempdb..#e_cust') IS NOT NULL DROP TABLE #e_cust;
    SELECT CustomerUuid = cust, Name = CONCAT('Customer ', n), City = CONCAT('City ', n)
    INTO #e_cust
    FROM #e_ids;

    IF OBJECT_ID('tempdb..#e_site') IS NOT NULL DROP TABLE #e_site;
    SELECT SiteUuid = site, Name = CONCAT('Site ', n), City = CONCAT('City ', n), CustomerUuid = cust
    INTO #e_site
    FROM #e_ids;

    IF OBJECT_ID('tempdb..#e_dev') IS NOT NULL DROP TABLE #e_dev;
    SELECT DeviceUuid = dev, Name = CONCAT('Device ', n), Location = CONCAT('Floor ', n), SiteUuid = site
    INTO #e_dev
    FROM #e_ids;

    IF OBJECT_ID('tempdb..#e_usr') IS NOT NULL DROP TABLE #e_usr;
    SELECT UserUuid = usr, Name = CONCAT('User ', n), Location = 'Ops'
    INTO #e_usr
    FROM #e_ids;

    IF OBJECT_ID('tempdb..#e_mea') IS NOT NULL DROP TABLE #e_mea;
    SELECT MeasurementUuid = mea, Name = CONCAT('Sensor ', n), Location = CONCAT('Room ', n),
           DeviceUuid = dev, Measurement = CAST(20.0 + (n*0.3) AS decimal(4,1))
    INTO #e_mea
    FROM #e_ids;

    ------------------------
    -- Seed with IF blocks --
    ------------------------

    -- Customers
    IF NOT EXISTS (SELECT 1 FROM dbo.Customer)
    BEGIN
        INSERT dbo.Customer (CustomerUuid, Name, City)
        SELECT CustomerUuid, Name, City FROM #c_cust
        UNION ALL
        SELECT CustomerUuid, Name, City FROM #e_cust;
    END

    -- Sites
    IF NOT EXISTS (SELECT 1 FROM dbo.Site)
    BEGIN
        INSERT dbo.Site (SiteUuid, Name, City, CustomerUuid)
        SELECT SiteUuid, Name, City, CustomerUuid FROM #c_site
        UNION ALL
        SELECT SiteUuid, Name, City, CustomerUuid FROM #e_site;
    END

    -- Devices
    IF NOT EXISTS (SELECT 1 FROM dbo.Device)
    BEGIN
        INSERT dbo.Device (DeviceUuid, Name, Location, SiteUuid)
        SELECT DeviceUuid, Name, Location, SiteUuid FROM #c_dev
        UNION ALL
        SELECT DeviceUuid, Name, Location, SiteUuid FROM #e_dev;
    END

    -- Users
    IF NOT EXISTS (SELECT 1 FROM dbo.[User])
    BEGIN
        INSERT dbo.[User] (UserUuid, Name, Location)
        SELECT UserUuid, Name, Location FROM #c_usr
        UNION ALL
        SELECT UserUuid, Name, Location FROM #e_usr;
    END

    -- Measurements
    IF NOT EXISTS (SELECT 1 FROM dbo.Measurement)
    BEGIN
        INSERT dbo.Measurement (MeasurementUuid, Name, Location, DeviceUuid, Measurement)
        SELECT MeasurementUuid, Name, Location, DeviceUuid, Measurement FROM #c_mea
        UNION ALL
        SELECT MeasurementUuid, Name, Location, DeviceUuid, Measurement FROM #e_mea;
    END

    -- SiteUser links: 2 per user (common→common, env→env)
    IF NOT EXISTS (SELECT 1 FROM dbo.SiteUser)
    BEGIN
        ;WITH cu AS (
            SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) rn, UserUuid FROM #c_usr
        ), cs AS (
            SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) rn, SiteUuid FROM #c_site
        ), eu AS (
            SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) rn, UserUuid FROM #e_usr
        ), es AS (
            SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) rn, SiteUuid FROM #e_site
        ), pairs AS (
            SELECT cu.UserUuid, cs.SiteUuid FROM cu JOIN cs ON cs.rn = cu.rn
            UNION ALL
            SELECT cu.UserUuid, cs.SiteUuid FROM cu JOIN cs ON cs.rn = CASE WHEN cu.rn = 5 THEN 1 ELSE cu.rn + 1 END
            UNION ALL
            SELECT eu.UserUuid, es.SiteUuid FROM eu JOIN es ON es.rn = eu.rn
            UNION ALL
            SELECT eu.UserUuid, es.SiteUuid FROM eu JOIN es ON es.rn = CASE WHEN eu.rn = 5 THEN 1 ELSE eu.rn + 1 END
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
