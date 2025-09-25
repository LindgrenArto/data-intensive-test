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
    }
}
