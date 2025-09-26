using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface ISiteService
    {
        List<SiteDTO> GetSites(DataStore store);

        SiteDTO GetSiteByUuid(DataStore store, string uuid);
        UpdateSiteDTO UpdateSite(DataStore store, UpdateSiteDTO site);
    }
}
