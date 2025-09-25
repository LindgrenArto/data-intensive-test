using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface IDeviceRepository
    {
        List<Device> GetDevices(DataStore store);
    }

}
