using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface ISiteRepository
    {
        List<Site> GetSites(DataStore store);
        Site GetSiteByUuid(DataStore store, string uuid);

        Site UpdateSite(DataStore store, Site site);
    }
}
