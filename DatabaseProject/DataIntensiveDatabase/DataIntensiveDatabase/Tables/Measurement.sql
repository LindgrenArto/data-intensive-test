CREATE TABLE [dbo].[Measurement]
(
	[Id] INT NOT NULL IDENTITY,
	[MeasurementUuid] NVARCHAR(24) PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Location] NVARCHAR(50) NULL,
	[DeviceUuid] NVARCHAR(24) NOT NULL,
	[Measurement] FLOAT NULL, 

    CONSTRAINT [FK_Measurement_DeviceUuid] FOREIGN KEY ([DeviceUuid]) REFERENCES [Device]([DeviceUuid])
)
