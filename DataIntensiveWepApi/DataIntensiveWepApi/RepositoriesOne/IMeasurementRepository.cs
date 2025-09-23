using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface IMeasurementRepository
    {
        List<Measurement> GetMeasurements();
    }
}
