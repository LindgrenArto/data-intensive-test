using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface ISiteService
    {
        List<SiteDTO> GetSites(DataStore store);
    }
}
