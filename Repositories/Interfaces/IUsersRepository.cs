using AmarBariAPI.Dtos.User;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<Result<List<UserDto>>> GetAllUsers();
        Task<Result<long>> Create(UserRequestDto dto);
        Task<Result<LoginResponseDto>> Login(LoginRequestDto dto);
    }
}
