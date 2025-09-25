using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IDeviceService
    {
        List<DeviceDTO> GetDevices(DataStore store);
    }
}
