using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IFlatRenterRepository
    {
        Task<Result<List<FlatRenterResponseDto>>> GetAllFlatRenters();
        Task<Result<FlatRenterResponseDto>> GetById(long id);
        Task<Result<List<FlatRenterResponseDto>>> GetByFlatId(long flatId);
        Task<Result<long>> Create(FlatRenterRequestDto dto);
        Task<Result<FlatRenterResponseDto>> Update(FlatRenterRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
