using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IUserService
    {
        List<UserDTO> GetUsers(DataStore store);
        UserDTO GetUserByUuid(DataStore store, string uuid);
        UserDTO UpdateUser(DataStore store, UserDTO userDTO);
    }
}
