using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IConnectionResolver _conn;

        public DeviceRepository(IConnectionResolver conn)
        {
            _conn = conn;
        }

        private DataIntensiveDatabaseContext Create(DataStore store)
        {
            var opts = new DbContextOptionsBuilder<DataIntensiveDatabaseContext>()
                .UseSqlServer(_conn.GetConnection(store))
                .Options;

            return new DataIntensiveDatabaseContext(opts);
        }
        public List<Device> GetDevices(DataStore store)
        {
            try
            {
                var db = Create(store);
                return db.Devices.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public Device GetDeviceByUuid(DataStore store, string uuid)
        {
            try
            {
                using var db = Create(store);

                Device device = db.Devices.Where(c => c.DeviceUuid == uuid).Single();

                return device;
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching device", e);
            }
        }

        public Device UpdateDevice(DataStore store, Device incoming)
        {
            try
            {
                using var db = Create(store);

                var original = db.Devices.Where(c => c.DeviceUuid == incoming.DeviceUuid).Single();

                original.Name = incoming.Name;
                original.Location = incoming.Location;

                db.Devices.Update(original);
                db.SaveChanges();

                return original;
            }
            catch (Exception e)
            {
                throw new Exception("Error updating device", e);
            }
        }
    }
}
