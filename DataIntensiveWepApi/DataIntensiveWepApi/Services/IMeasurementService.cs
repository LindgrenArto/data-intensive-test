using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IMeasurementService
    {
        List<MeasurementDTO> GetMeasurements(DataStore store);

        MeasurementDTO GetMeasurementByUuid(DataStore store, string uuid);
        MeasurementDTO UpdateMeasurement(DataStore store, MeasurementDTO measurementDTO);
    }
}
