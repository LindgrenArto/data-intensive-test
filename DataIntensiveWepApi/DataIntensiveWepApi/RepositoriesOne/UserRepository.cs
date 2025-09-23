using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public class UserRepository : IUserRepository
    {
        private readonly DataIntensiveDatabase1Context _context;

        public UserRepository(DataIntensiveDatabase1Context context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
