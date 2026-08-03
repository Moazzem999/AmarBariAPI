using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Services.Interfaces;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services
{
    public class ShopService(IShopsRepository shopsRepository) : IShopService
    {
        public async Task<Result<List<ShopResponseDto>>> GetAllShops()
        {
            return await shopsRepository.GetAllShops();
        }

        public async Task<Result<ShopResponseDto>> GetById(long id)
        {
            return await shopsRepository.GetById(id);
        }

        public async Task<Result<long>> Create(ShopRequestDto dto)
        {
            return await shopsRepository.Create(dto);
        }

        public async Task<Result<ShopResponseDto>> Update(ShopRequestDto dto)
        {
            return await shopsRepository.Update(dto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            return await shopsRepository.Delete(id);
        }
    }
}
