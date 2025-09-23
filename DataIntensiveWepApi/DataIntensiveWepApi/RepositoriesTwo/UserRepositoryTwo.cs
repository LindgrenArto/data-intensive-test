using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesTwo
{
    public class UserRepositoryTwo : IUserRepositoryTwo
    {
        private readonly DataIntensiveDatabase2Context _context;

        public UserRepositoryTwo(DataIntensiveDatabase2Context context)
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
