using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesTwo
{

    public class MeasurementRepositoryTwo : IMeasurementRepositoryTwo
    {
        private readonly DataIntensiveDatabase2Context _context;

        public MeasurementRepositoryTwo(DataIntensiveDatabase2Context context)
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
