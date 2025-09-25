using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;
using System.Collections.Generic;
using System.Linq;

namespace DataIntensiveWepApi.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;

        public SiteService(ISiteRepository siteRepositoryOne, IMapper mapper)
        {
            _siteRepository = siteRepositoryOne;
            _mapper = mapper;
        }

        public List<SiteDTO> GetSites(DataStore store)
        {
            try
            {
                List<SiteDTO> siteDTO = _mapper.Map<List<SiteDTO>>(_siteRepository.GetSites(store));
   
                return siteDTO;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
