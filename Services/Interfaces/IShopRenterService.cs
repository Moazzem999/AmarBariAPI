using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services.Interfaces
{
    public interface IShopRenterService
    {
        Task<Result<List<ShopRenterResponseDto>>> GetAllShopRenters();
        Task<Result<long>> Create(ShopRenterRequestDto dto);
        Task<Result<ShopRenterResponseDto>> Update(ShopRenterRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
