using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;

namespace DataIntensiveWepApi.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public List<CustomerDTO> GetCustomers(DataStore store)
        {
            try
            {
                var customers = _mapper.Map<List<CustomerDTO>>(_customerRepository.GetCustomers(store));
                return customers;
            }
            catch (Exception e)
            {

                throw new Exception("Error", e);
            }
        }
    }
}
