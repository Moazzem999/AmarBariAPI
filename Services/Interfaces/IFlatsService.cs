using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services.Interfaces
{
    public interface IFlatsService
    {
        Task<Result<List<FlatResponseDto>>> GetAllFlats();
        Task<Result<FlatResponseDto>> GetById(long id);
        Task<Result<long>> Create(FlatRequestDto dto);
        Task<Result<FlatResponseDto>> Update(FlatRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
