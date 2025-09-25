using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
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
    }
}
