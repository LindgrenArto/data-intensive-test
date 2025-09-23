using DataIntensiveWepApi.Models;

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
                return _context.Sites.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
