using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IHomesRepository
    {
        Task<Result<List<HomeResponseDto>>> GetAllHomes();
        Task<Result<HomeResponseDto>> GetById(long id);
        Task<Result<long>> Create(HomeRequestDto dto);
        Task<Result<HomeResponseDto>> Update(HomeRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
