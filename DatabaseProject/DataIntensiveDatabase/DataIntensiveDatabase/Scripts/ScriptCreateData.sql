SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    -- Optional SQLCMD variable (set in publish profile). Falls back to DB name hash if empty.
    DECLARE @EnvTag nvarchar(16) = '$(EnvTag)';
    IF NULLIF(@EnvTag,'') IS NULL
        SELECT @EnvTag = UPPER(SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA1', DB_NAME())), 3, 4));

    -- Small per-environment numeric bump for measurements (0.0 .. 0.9)
    DECLARE @EnvBump decimal(4,1) = CAST((ABS(CHECKSUM(@EnvTag)) % 10) AS decimal(4,1)) / 10.0;

    -- Choose one of three city sets based on EnvTag (1=Sweden, 2=Norway, 3=Denmark)
    DECLARE @CitySet int = ((ABS(CHECKSUM(@EnvTag)) % 3) + 1);

    -------------------------
    -- Build temp datasets --
    -------------------------
    IF OBJECT_ID('tempdb..#nums') IS NOT NULL DROP TABLE #nums;
    SELECT n INTO #nums
    FROM (VALUES (1),(2),(3),(4),(5),(6),(7),(8),(9),(10)) v(n);

    -- Common rows 1..5 (identical across DBs)
    IF OBJECT_ID('tempdb..#c_cust') IS NOT NULL DROP TABLE #c_cust;
    SELECT
        CustomerUuid = CONCAT('CUST', REPLICATE('0', 24-4-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Name = CONCAT('Customer ', n),
        City = CHOOSE(n, N'Helsinki', N'Espoo', N'Vantaa', N'Tampere', N'Oulu', N'Turku', N'Lahti', N'Kuopio', N'Jyväskylä', N'Joensuu')
    INTO #c_cust
    FROM #nums WHERE n <= 5;

    IF OBJECT_ID('tempdb..#c_site') IS NOT NULL DROP TABLE #c_site;
    SELECT
        SiteUuid = CONCAT('SITE', REPLICATE('0', 24-4-3), RIGHT('00'+CAST(n AS varchar(3)),3)),
        Name = CONCAT('Site ', n),
        City = CHOOSE(n, N'Helsinki', N'Espoo', N'Vantaa', N'Tampere', N'Oulu', N'Turku', N'Lahti', N'Kuopio', N'Jyväskylä', N'Joensuu'),
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
        Location = N'Ops'
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

    -- Env-specific rows 6..10 (UUIDs incorporate @EnvTag)
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

    -- Materialize env mapping and pick cities per rn (1..5) from the chosen city set
    IF OBJECT_ID('tempdb..#e') IS NOT NULL DROP TABLE #e;
    SELECT n, cust, site, dev, usr, mea,
           ROW_NUMBER() OVER (ORDER BY n) AS rn
    INTO #e
    FROM #e_ids;

    IF OBJECT_ID('tempdb..#cities') IS NOT NULL DROP TABLE #cities;
    ;WITH rows(rn) AS (
        SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5
    )
    SELECT r.rn,
           CityName = CASE @CitySet
                        WHEN 1 THEN CHOOSE(r.rn, N'Stockholm', N'Göteborg', N'Malmö', N'Uppsala', N'Västerås')
                        WHEN 2 THEN CHOOSE(r.rn, N'Oslo', N'Bergen', N'Trondeim', N'Stavanger', N'Drammen')
                        WHEN 3 THEN CHOOSE(r.rn, N'København', N'Aarhus', N'Odense', N'Aalborg', N'Esbjerg')
                      END
    INTO #cities
    FROM rows r;

    -- Customers (env: city from #cities)
    IF OBJECT_ID('tempdb..#e_cust') IS NOT NULL DROP TABLE #e_cust;
    SELECT
        CustomerUuid = e.cust,
        Name        = CONCAT('Customer ', e.n, ' - ', @EnvTag),
        City        = c.CityName
    INTO #e_cust
    FROM #e e
    JOIN #cities c ON c.rn = e.rn;

    -- Sites
    IF OBJECT_ID('tempdb..#e_site') IS NOT NULL DROP TABLE #e_site;
    SELECT
        SiteUuid     = e.site,
        Name         = CONCAT('Site ', e.n, ' - ', @EnvTag),
        City         = c.CityName,
        CustomerUuid = e.cust
    INTO #e_site
    FROM #e e
    JOIN #cities c ON c.rn = e.rn;

    -- Devices (Location includes city)
    IF OBJECT_ID('tempdb..#e_dev') IS NOT NULL DROP TABLE #e_dev;
    SELECT
        DeviceUuid = e.dev,
        Name      = CONCAT('Device ', e.n, ' (', @EnvTag, ')'),
        Location  = CONCAT('Zone ', e.rn, N' – ', c.CityName),
        SiteUuid  = e.site
    INTO #e_dev
    FROM #e e
    JOIN #cities c ON c.rn = e.rn;

    -- Users (Location shows env + city)
    IF OBJECT_ID('tempdb..#e_usr') IS NOT NULL DROP TABLE #e_usr;
    SELECT
        UserUuid = e.usr,
        Name     = CONCAT('User ', e.n, ' ', @EnvTag),
        Location = CONCAT(N'Ops-', @EnvTag, N'-', c.CityName)
    INTO #e_usr
    FROM #e e
    JOIN #cities c ON c.rn = e.rn;

    -- Measurements (city + env bump)
    IF OBJECT_ID('tempdb..#e_mea') IS NOT NULL DROP TABLE #e_mea;
    SELECT
        MeasurementUuid = e.mea,
        Name            = CONCAT('Sensor ', e.n, ' ', @EnvTag),
        Location        = CONCAT('Room ', e.rn, N' / ', c.CityName),
        DeviceUuid      = e.dev,
        Measurement     = CAST(20.0 + (e.n*0.3) + @EnvBump AS decimal(4,1))
    INTO #e_mea
    FROM #e e
    JOIN #cities c ON c.rn = e.rn;

    ------------------------
    -- Seed with IF blocks --
    ------------------------

    IF NOT EXISTS (SELECT 1 FROM dbo.Customer)
    BEGIN
        INSERT dbo.Customer (CustomerUuid, Name, City)
        SELECT CustomerUuid, Name, City FROM #c_cust
        UNION ALL
        SELECT CustomerUuid, Name, City FROM #e_cust;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Site)
    BEGIN
        INSERT dbo.Site (SiteUuid, Name, City, CustomerUuid)
        SELECT SiteUuid, Name, City, CustomerUuid FROM #c_site
        UNION ALL
        SELECT SiteUuid, Name, City, CustomerUuid FROM #e_site;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Device)
    BEGIN
        INSERT dbo.Device (DeviceUuid, Name, Location, SiteUuid)
        SELECT DeviceUuid, Name, Location, SiteUuid FROM #c_dev
        UNION ALL
        SELECT DeviceUuid, Name, Location, SiteUuid FROM #e_dev;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.[User])
    BEGIN
        INSERT dbo.[User] (UserUuid, Name, Location)
        SELECT UserUuid, Name, Location FROM #c_usr
        UNION ALL
        SELECT UserUuid, Name, Location FROM #e_usr;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Measurement)
    BEGIN
        INSERT dbo.Measurement (MeasurementUuid, Name, Location, DeviceUuid, Measurement)
        SELECT MeasurementUuid, Name, Location, DeviceUuid, Measurement FROM #c_mea
        UNION ALL
        SELECT MeasurementUuid, Name, Location, DeviceUuid, Measurement FROM #e_mea;
    END

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
            -- common users: link to site i and next (wrap at 5)
            SELECT cu.UserUuid, cs.SiteUuid FROM cu JOIN cs ON cs.rn = cu.rn
            UNION ALL
            SELECT cu.UserUuid, cs.SiteUuid FROM cu JOIN cs ON cs.rn = CASE WHEN cu.rn = 5 THEN 1 ELSE cu.rn + 1 END
            UNION ALL
            -- env users: link to site i and next (wrap at 5)
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
