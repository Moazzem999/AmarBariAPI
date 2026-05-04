using AmarBariAPI.Dtos.User;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Services.Interfaces;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services
{
    public class UserService(IUsersRepository usersRepository) : IUserService
    {
        public async Task<Result<List<UserDto>>> GetAllUsers()
        {
            return await usersRepository.GetAllUsers();
        }

        public async Task<Result<long>> Create(UserRequestDto dto)
        {
            return await usersRepository.Create(dto);
        }

        public async Task<Result<LoginResponseDto>> Login(LoginRequestDto dto)
        {
            return await usersRepository.Login(dto);
        }
    }
}
