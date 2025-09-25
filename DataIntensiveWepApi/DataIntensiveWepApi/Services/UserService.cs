using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.RepositoriesOne;

namespace DataIntensiveWepApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
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
                        users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers(DataStore.One));
                        break;
                    case 2:
                        users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers(DataStore.Two));
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
