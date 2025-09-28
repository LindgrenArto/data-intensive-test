using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;

namespace DataIntensiveWepApi.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepositoryOne, IMapper mapper)
        {
            _deviceRepository = deviceRepositoryOne;
            _mapper = mapper;
        }

        public List<DeviceDTO> GetDevices(DataStore store)
        {
            try
            {
                List<DeviceDTO> devices = _mapper.Map<List<DeviceDTO>>(_deviceRepository.GetDevices(store));
 
                return devices;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public DeviceDTO GetDeviceByUuid(DataStore store, string uuid)
        {
            try
            {
                DeviceDTO device = _mapper.Map<DeviceDTO>(_deviceRepository.GetDeviceByUuid(store, uuid));

                return device;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public DeviceDTO UpdateDevice(DataStore store, DeviceDTO incoming)
        {
            try
            {
                Device device = _mapper.Map<Device>(incoming);
                DeviceDTO updated = _mapper.Map<DeviceDTO>(_deviceRepository.UpdateDevice(store, device));

                return updated;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
