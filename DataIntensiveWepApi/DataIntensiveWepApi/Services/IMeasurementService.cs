using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IMeasurementService
    {
        List<MeasurementDTO> GetMeasurements(int db);
    }
}
