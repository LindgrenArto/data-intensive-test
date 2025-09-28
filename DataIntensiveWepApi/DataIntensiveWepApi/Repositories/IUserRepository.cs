using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface IUserRepository
    {
        List<User> GetUsers(DataStore store);

        User GetUserByUuid(DataStore store, string uuid);

        User UpdateUser(DataStore store, User user);
    }
}
