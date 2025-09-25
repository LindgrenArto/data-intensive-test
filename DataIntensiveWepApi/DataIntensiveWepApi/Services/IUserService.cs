using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IUserService
    {
        List<UserDTO> GetUsers(DataStore store);
    }
}
