using DataIntensiveWepApi.DTOModels;

namespace DataIntensiveWepApi.Services
{
    public interface ISiteService
    {
        List<SiteDTO> GetSites(int db);
    }
}
