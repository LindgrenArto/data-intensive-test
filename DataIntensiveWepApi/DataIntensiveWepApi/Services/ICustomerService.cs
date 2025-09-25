using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface ICustomerService
    {
        List<CustomerDTO> GetCustomers(DataStore store);
    }
}
