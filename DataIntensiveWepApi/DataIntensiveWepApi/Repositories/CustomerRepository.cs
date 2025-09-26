using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public Customer GetCustomerByUuid(DataStore store, string uuid)
        {
            try
            {
                using var db = Create(store);

                Customer customer = db.Customers.Where(c => c.CustomerUuid == uuid).Single();

                return customer;
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching customer", e);
            }
        }

        public Customer UpdateCustomer(DataStore store, Customer incoming)
        {
            {
                try
                {
                    using var db = Create(store);

                    var original = db.Customers.Where(c => c.CustomerUuid == incoming.CustomerUuid).FirstOrDefault();

                    original.Name = incoming.Name;
                    original.City = incoming.City;

                    db.Customers.Update(original);
                    db.SaveChanges();

                    return original;
                }
                catch (Exception e)
                {
                    throw new Exception("Error updating customer", e);
                }
            }
        }
    }
}
