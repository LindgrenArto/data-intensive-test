using AutoMapper;
using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
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


        public List<UserDTO> GetUsers(DataStore store)
        {
            try
            {
                List<UserDTO> users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers(store));           
                return users;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public UserDTO GetUserByUuid(DataStore store, string uuid)
        {
            try
            {
                UserDTO user = _mapper.Map<UserDTO>(_userRepository.GetUserByUuid(store, uuid));
                return user;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }


        public UserDTO UpdateUser(DataStore store, UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                UserDTO updated = _mapper.Map<UserDTO>(_userRepository.UpdateUser(store, user));
                return updated;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }
    }
}
