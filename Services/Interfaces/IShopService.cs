using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services.Interfaces
{
    public interface IShopService
    {
        Task<Result<List<ShopResponseDto>>> GetAllShops();
        Task<Result<ShopResponseDto>> GetById(long id);
        Task<Result<long>> Create(ShopRequestDto dto);
        Task<Result<ShopResponseDto>> Update(ShopRequestDto dto);
        Task<Result<string>> Delete(int id);
    }
}
