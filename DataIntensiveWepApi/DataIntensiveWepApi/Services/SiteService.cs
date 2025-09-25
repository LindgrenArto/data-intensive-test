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

        public List<SiteDTO> GetSites(int db)
        {
            try
            {
                List<SiteDTO> siteDTO = new List<SiteDTO>();
                switch (db)
                {
                    case 1:
                       siteDTO = _mapper.Map<List<SiteDTO>>(_siteRepository.GetSites(DataStore.One));
                        break;
                    case 2:
                        siteDTO = _mapper.Map<List<SiteDTO>>(_siteRepository.GetSites(DataStore.Two));
                        break;
                }

                return siteDTO;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
