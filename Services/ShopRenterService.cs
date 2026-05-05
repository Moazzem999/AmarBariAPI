using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Services.Interfaces;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services
{
    public class ShopRenterService(IShopRenterRepository shopRenterRepository) : IShopRenterService
    {
        private readonly IShopRenterRepository shopRenterRepository = shopRenterRepository;

        public async Task<Result<List<ShopRenterResponseDto>>> GetAllShopRenters()
        {
            return await shopRenterRepository.GetAllShopRenters();
        }

        public async Task<Result<long>> Create(ShopRenterRequestDto dto)
        {
            return await shopRenterRepository.Create(dto);
        }

        public async Task<Result<ShopRenterResponseDto>> Update(ShopRenterRequestDto dto)
        {
            return await shopRenterRepository.Update(dto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            return await shopRenterRepository.Delete(id);
        }
    }
}
