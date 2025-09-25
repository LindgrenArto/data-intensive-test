using AutoMapper;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.RepositoriesOne;
using DataIntensiveWepApi.RepositoriesTwo;
using System.Collections.Generic;
using System.Linq;

namespace DataIntensiveWepApi.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepositoryOne;
        private readonly ISiteRepositoryTwo _siteRepositoryTwo;
        private readonly IMapper _mapper;

        public SiteService(ISiteRepository siteRepositoryOne, ISiteRepositoryTwo siteRepositoryTwo, IMapper mapper)
        {
            _siteRepositoryOne = siteRepositoryOne;
            _siteRepositoryTwo = siteRepositoryTwo;
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
                       siteDTO = _mapper.Map<List<SiteDTO>>(_siteRepositoryOne.GetSites());
                        break;
                    case 2:
                        siteDTO = _mapper.Map<List<SiteDTO>>(_siteRepositoryTwo.GetSites());
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
