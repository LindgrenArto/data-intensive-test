using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IDeviceService
    {
        List<DeviceDTO> GetDevices(DataStore store);

        DeviceDTO GetDeviceByUuid(DataStore store, string uuid);

        DeviceDTO UpdateDevice(DataStore store, DeviceDTO device);
    }
}
