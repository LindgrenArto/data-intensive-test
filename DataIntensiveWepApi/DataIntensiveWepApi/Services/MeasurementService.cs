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

        public List<MeasurementDTO> GetMeasurements(int db)
        {
            try
            {
                List<MeasurementDTO> measurements = new List<MeasurementDTO>();
                switch (db)
                {
                    case 1:
                        measurements = _mapper.Map<List<MeasurementDTO>>(_measurementRepository.GetMeasurements(DataStore.One));
                        break;
                    case 2:
                        measurements = _mapper.Map<List<MeasurementDTO>>(_measurementRepository.GetMeasurements(DataStore.Two));
                        break;
                }

                return measurements;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
