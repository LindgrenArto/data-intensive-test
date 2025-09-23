using AutoMapper;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.RepositoriesOne;
using DataIntensiveWepApi.RepositoriesTwo;

namespace DataIntensiveWepApi.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepositoryOne;
        private readonly IDeviceRepositoryTwo _deviceRepositoryTwo;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepositoryOne, IDeviceRepositoryTwo deviceRepositoryTwo, IMapper mapper)
        {
            _deviceRepositoryOne = deviceRepositoryOne;
            _deviceRepositoryTwo = deviceRepositoryTwo;
            _mapper = mapper;
        }

        public List<DeviceDTO> GetDevices(int db)
        {
            try
            {
                List<DeviceDTO> devices = new List<DeviceDTO>();
                switch (db)
                {
                    case 1:
                        devices = _mapper.Map<List<DeviceDTO>>(_deviceRepositoryOne.GetDevices());
                        break;
                    case 2:
                        devices = _mapper.Map<List<DeviceDTO>>(_deviceRepositoryTwo.GetDevices());
                        break;
                }

                return devices;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
