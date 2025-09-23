using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataIntensiveDatabase1Context _context;

        public DeviceRepository(DataIntensiveDatabase1Context context)
        {
            _context = context;
        }

        public List<Device> GetDevices()
        {
            try
            {
                return _context.Devices.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
