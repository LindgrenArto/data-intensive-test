using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers(DataStore store);
    }
}
