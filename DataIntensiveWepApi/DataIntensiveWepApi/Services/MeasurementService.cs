using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
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

        public MeasurementDTO GetMeasurementByUuid(DataStore store, string uuid)
        {
            try
            {
                MeasurementDTO measurement = _mapper.Map<MeasurementDTO>(_measurementRepository.GetMeasurementByUuid(store, uuid));
                return measurement;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }


        public MeasurementDTO UpdateMeasurement(DataStore store, MeasurementDTO measurementDTO)
        {
            try
            {
                Measurement measurement = _mapper.Map<Measurement>(measurementDTO);
                MeasurementDTO updated = _mapper.Map<MeasurementDTO>(_measurementRepository.UpdateMeasurement(store, measurement));
                return updated;

            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
