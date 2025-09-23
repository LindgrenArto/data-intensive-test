using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface IDeviceRepository
    {
        List<Device> GetDevices();
    }

}
