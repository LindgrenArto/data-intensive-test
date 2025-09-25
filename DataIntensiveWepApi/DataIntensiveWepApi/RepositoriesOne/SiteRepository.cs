using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public class SiteRepository : ISiteRepository
    {
        private readonly DataIntensiveDatabase1Context _context;

        public SiteRepository(DataIntensiveDatabase1Context context)
        {
            _context = context;
        }

        public List<Site> GetSites()
        {
            try
            {
                var sites = _context.Sites
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
