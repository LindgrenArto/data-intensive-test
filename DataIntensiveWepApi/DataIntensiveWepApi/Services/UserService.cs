using AutoMapper;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.RepositoriesOne;
using DataIntensiveWepApi.RepositoriesTwo;

namespace DataIntensiveWepApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepositoryOne;
        private readonly IUserRepositoryTwo _userRepositoryTwo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepositoryOne, IUserRepositoryTwo userRepositoryTwo, IMapper mapper)
        {
            _userRepositoryOne = userRepositoryOne;
            _userRepositoryTwo = userRepositoryTwo;
            _mapper = mapper;
        }

        public List<UserDTO> GetUsers(int db)
        {
            try
            {
                List<UserDTO> users = new List<UserDTO>();
                switch (db)
                {
                    case 1:
                        users = _mapper.Map<List<UserDTO>>(_userRepositoryOne.GetUsers());
                        break;
                    case 2:
                        users = _mapper.Map<List<UserDTO>>(_userRepositoryTwo.GetUsers());
                        break;
                }

                return users;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
