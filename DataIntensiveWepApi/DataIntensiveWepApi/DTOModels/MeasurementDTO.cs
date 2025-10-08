namespace DataIntensiveWepApi.DTOModels
{
    public class MeasurementDTO
    {
        public string MeasurementUuid { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double? Measurement1 { get; set; }

        public string DeviceUuid { get; set; }

    }
}
