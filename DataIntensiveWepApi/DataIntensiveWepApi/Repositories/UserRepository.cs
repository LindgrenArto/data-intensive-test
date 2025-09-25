using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionResolver _conn;

        public UserRepository(IConnectionResolver conn)
        {
            _conn = conn;
        }

        private DataIntensiveDatabaseContext Create(DataStore store)
        {
            var opts = new DbContextOptionsBuilder<DataIntensiveDatabaseContext>()
                .UseSqlServer(_conn.GetConnection(store))
                .Options;

            return new DataIntensiveDatabaseContext(opts);
        }

        public List<User> GetUsers(DataStore store)
        {
            try
            {
                using var db = Create(store);
                return db.Users.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
