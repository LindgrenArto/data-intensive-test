using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface IUserRepository
    {
        List<User> GetUsers();
    }
}
