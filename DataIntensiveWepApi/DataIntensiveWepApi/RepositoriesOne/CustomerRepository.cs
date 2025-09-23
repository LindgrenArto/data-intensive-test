using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataIntensiveDatabase2Context _context;

        public CustomerRepository(DataIntensiveDatabase2Context context)
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
