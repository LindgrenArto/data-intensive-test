using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesTwo
{
    public class SiteRepositoryTwo : ISiteRepositoryTwo
    {
        private readonly DataIntensiveDatabase2Context _context;

        public SiteRepositoryTwo(DataIntensiveDatabase2Context context)
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
