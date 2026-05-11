using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IFlatsRepository
    {
        Task<Result<List<FlatResponseDto>>> GetAllFlats();
        Task<Result<FlatResponseDto>> GetById(long id);
        Task<Result<List<FlatResponseDto>>> GetByHomeId(long homeId);
        Task<Result<long>> Create(FlatRequestDto dto);
        Task<Result<FlatResponseDto>> Update(FlatRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
