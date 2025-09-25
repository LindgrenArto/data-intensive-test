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

        public List<CustomerDTO> GetCustomers(int db)
        {
            try
            {
                List<CustomerDTO> customers = new List<CustomerDTO>();
                switch (db)
                {
                    case 1:
                        customers = _mapper.Map<List<CustomerDTO>>(_customerRepository.GetCustomers(DataStore.One));
                        break;
                    case 2:
                        customers = _mapper.Map<List<CustomerDTO>>(_customerRepository.GetCustomers(DataStore.Two));
                        break;
                }

               return customers;
            }
            catch (Exception e)
            {

                throw new Exception("Error", e);
            }
        }
    }
}
