using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.Services
{
    public interface ICustomerService
    {
        List<CustomerDTO> GetCustomers(DataStore store);
        CustomerDTO GetCustomerByUuid(DataStore store, string uuid);
        CustomerDTO UpdateCustomer(DataStore store, CustomerDTO customer);
    }
}
