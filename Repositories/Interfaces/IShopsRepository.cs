using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IShopsRepository
    {
        Task<Result<List<ShopResponseDto>>> GetAllShops();
        Task<Result<ShopResponseDto>> GetById(long id);
        Task<Result<long>> Create(ShopRequestDto dto);
        Task<Result<ShopResponseDto>> Update(ShopRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
