using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
    }
}
