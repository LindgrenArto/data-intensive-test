using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public class SiteRepository : ISiteRepository
    {
        private readonly IConnectionResolver _conn;

        public SiteRepository(IConnectionResolver conn)
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

        public List<Site> GetSites(DataStore store)
        {
            try
            {
                using var db = Create(store);
                var sites = db.Sites
                        .Include(s => s.SiteUsers)
                        .ThenInclude(su => su.UserUu)
                        .ToList();
                return sites;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
