using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;
using Microsoft.EntityFrameworkCore;

namespace DataIntensiveWepApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConnectionResolver _conn;

        public CustomerRepository(IConnectionResolver conn)
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

        public List<Customer> GetCustomers(DataStore store)
        {
            try
            {
                using var db = Create(store);   
                return db.Customers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching customers", e);
            }
        }
    }
}
