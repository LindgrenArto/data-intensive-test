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

        public Site GetSiteByUuid(DataStore store, string uuid)
        {
            try
            {
                using var db = Create(store);

                Site site = db.Sites.Where(s => s.SiteUuid == uuid).Single();

                return site;
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching site", e);
            }
        }

        public Site UpdateSite(DataStore store, Site incoming)
        {
            {
                try
                {
                    using var db = Create(store);

                    var original = db.Sites.Where(s => s.SiteUuid == incoming.SiteUuid).FirstOrDefault();

                    original.Name = incoming.Name;
                    original.City = incoming.City;

                    db.Sites.Update(original);
                    db.SaveChanges();

                    return original;
                }
                catch (Exception e)
                {
                    throw new Exception("Error updating site", e);
                }
            }
        }
    }
}
