using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface IUserService
    {
        List<UserDTO> GetUsers(int db);
    }
}
