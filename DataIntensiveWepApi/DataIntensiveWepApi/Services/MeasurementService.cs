using AutoMapper;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.RepositoriesOne;
using DataIntensiveWepApi.RepositoriesTwo;

namespace DataIntensiveWepApi.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepositoryOne;
        private readonly IMeasurementRepositoryTwo _measurementRepositoryTwo;
        private readonly IMapper _mapper;

        public MeasurementService(IMeasurementRepository measurementRepositoryOne, IMeasurementRepositoryTwo measurementRepositoryTwo, IMapper mapper)
        {
            _measurementRepositoryOne = measurementRepositoryOne;
            _measurementRepositoryTwo = measurementRepositoryTwo;
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
                        measurements = _mapper.Map<List<MeasurementDTO>>(_measurementRepositoryOne.GetMeasurements());
                        break;
                    case 2:
                        measurements = _mapper.Map<List<MeasurementDTO>>(_measurementRepositoryTwo.GetMeasurements());
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
