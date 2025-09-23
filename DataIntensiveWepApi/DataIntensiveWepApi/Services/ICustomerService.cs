using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers(int db);
    }
}
