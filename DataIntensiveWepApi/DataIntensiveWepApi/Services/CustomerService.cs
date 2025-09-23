using AutoMapper;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;

namespace DataIntensiveWepApi.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepositoryOne;
        private readonly ICustomerRepository _customerRepositoryTwo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepositoryOne, ICustomerRepository customerRepositoryTwo, IMapper mapper)
        {
            _customerRepositoryOne = customerRepositoryOne;
            _customerRepositoryTwo = customerRepositoryTwo;
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
                        customers = _mapper.Map<List<CustomerDTO>>(_customerRepositoryOne.GetCustomers());
                        break;
                    case 2:
                        customers = _mapper.Map<List<CustomerDTO>>(_customerRepositoryTwo.GetCustomers());
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
