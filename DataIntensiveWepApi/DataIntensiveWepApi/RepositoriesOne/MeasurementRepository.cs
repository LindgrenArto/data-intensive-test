using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{

    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly DataIntensiveDatabase1Context _context;

        public MeasurementRepository(DataIntensiveDatabase1Context context)
        {
            _context = context;
        }

        public List<Measurement> GetMeasurements()
        {
            try
            {
                return _context.Measurements.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
