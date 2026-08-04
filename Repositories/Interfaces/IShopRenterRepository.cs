using AmarBariAPI.Dtos.Common;
using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IShopRenterRepository
    {
        Task<Result<List<ShopRenterResponseDto>>> GetAllShopRenters();
        Task<Result<ShopRenterResponseDto>> GetById(long id);
        Task<Result<List<ShopRenterResponseDto>>> GetByShopId(long shopId);
        Task<Result<long>> Create(ShopRenterRequestDto dto);
        Task<Result<ShopRenterResponseDto>> Update(ShopRenterRequestDto dto);
        Task<Result<string>> Delete(long id);
        Task<Result<List<DropdownDto>>> GetAllMaritalStatus();
        Task<Result<List<DropdownDto>>> GetAllReligion();
    }
}
