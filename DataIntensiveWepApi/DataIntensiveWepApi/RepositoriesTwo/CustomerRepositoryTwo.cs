using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesTwo
{
    public class CustomerRepositoryTwo : ICustomerRepositoryTwo
    {
        private readonly DataIntensiveDatabase2Context _context;

        public CustomerRepositoryTwo(DataIntensiveDatabase2Context context)
        {
            _context = context;
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> customers = _context.Customers.ToList();


                return customers;
            }
            catch (Exception e)
            {

                throw new Exception("Error", e);
            }
        }
    }
}
