using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

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

        public User GetUserByUuid(DataStore store, string uuid)
        {
            try
            {
                using var db = Create(store);

                User user = db.Users.Where(u => u.UserUuid == uuid).Single();

                return user;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public User UpdateUser(DataStore store, User incoming)
        {
            try
            {
                using var db = Create(store);

                var original = db.Users.Where(u => u.UserUuid == incoming.UserUuid).Single();

                original.Name = incoming.Name;
                original.Location = incoming.Location;

                db.Users.Update(original);
                db.SaveChanges();

                return original;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
