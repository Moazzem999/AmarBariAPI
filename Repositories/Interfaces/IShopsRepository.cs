using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IShopsRepository
    {
        Task<Result<List<ShopResponseDto>>> GetAllShops();
    }
}
