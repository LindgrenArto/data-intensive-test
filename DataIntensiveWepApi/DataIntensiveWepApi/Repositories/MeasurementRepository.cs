using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DataIntensiveWepApi.RepositoriesOne
{

    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly IConnectionResolver _conn;

        public MeasurementRepository(IConnectionResolver conn)
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

        public List<Measurement> GetMeasurements(DataStore store)
        {
            try
            {
                var db = Create(store);
                return db.Measurements.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public Measurement GetMeasurementByUuid(DataStore store, string uuid)
        {
            try
            {
                using var db = Create(store);

                Measurement measurement = db.Measurements.Where(m => m.MeasurementUuid == uuid).Single();

                return measurement;
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching measurement", e);
            }
        }

        public Measurement UpdateMeasurement(DataStore store, Measurement incoming)
        {
            try
            {
                using var db = Create(store);

                var original = db.Measurements.Where(m => m.MeasurementUuid == incoming.MeasurementUuid).Single();

                original.Measurement1 = incoming.Measurement1;
                original.Name = incoming.Name;
                original.Location = incoming.Location;

                db.Measurements.Update(original);
                db.SaveChanges();

                return original;
            }
            catch (Exception e)
            {
                throw new Exception("Error updating measurement", e);
            }
        }
    }
}
