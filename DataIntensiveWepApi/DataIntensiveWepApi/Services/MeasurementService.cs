using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.RepositoriesOne;

namespace DataIntensiveWepApi.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;

        public MeasurementService(IMeasurementRepository measurementRepository, IMapper mapper)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
        }

        public List<MeasurementDTO> GetMeasurements(DataStore store)
        {
            try
            {
                List<MeasurementDTO> measurements = _mapper.Map<List<MeasurementDTO>>(_measurementRepository.GetMeasurements(store));

                return measurements;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
