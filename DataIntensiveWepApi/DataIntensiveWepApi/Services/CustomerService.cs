using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;

namespace DataIntensiveWepApi.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepositoryOne;
        private readonly ICustomerRepository _customerRepositoryTwo;

        public CustomerService(ICustomerRepository customerRepositoryOne, ICustomerRepository customerRepositoryTwo)
        {
            _customerRepositoryOne = customerRepositoryOne;
            _customerRepositoryTwo = customerRepositoryTwo;
        }

        public List<Customer> GetCustomers(int db)
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                switch (db)
                {
                    case 1:
                        customers = _customerRepositoryOne.GetCustomers();
                        break;
                    case 2:
                        customers = _customerRepositoryTwo.GetCustomers();
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
