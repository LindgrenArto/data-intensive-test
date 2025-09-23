using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IDeviceService
    {
        List<DeviceDTO> GetDevices(int db);
    }
}
