using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers(DataStore store);

        Customer GetCustomerByUuid(DataStore store, string uuid);

        Customer UpdateCustomer(DataStore store, Customer customer);
    }
}
