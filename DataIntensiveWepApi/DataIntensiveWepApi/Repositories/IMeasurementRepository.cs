using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface IMeasurementRepository
    {
        List<Measurement> GetMeasurements(DataStore store);

        Measurement GetMeasurementByUuid(DataStore store, string uuid);

        Measurement UpdateMeasurement(DataStore store, Measurement measurement);
    }
}
