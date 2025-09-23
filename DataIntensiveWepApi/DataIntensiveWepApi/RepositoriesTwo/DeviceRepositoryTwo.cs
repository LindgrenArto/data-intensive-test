using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesTwo
{
    public class DeviceRepositoryTwo : IDeviceRepositoryTwo
    {
        private readonly DataIntensiveDatabase2Context _context;

        public DeviceRepositoryTwo(DataIntensiveDatabase2Context context)
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
