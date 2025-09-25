using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi.RepositoriesOne
{
    public interface ISiteRepository
    {
        List<Site> GetSites(DataStore store);
    }
}
