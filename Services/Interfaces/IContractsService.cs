using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services.Interfaces
{
    public interface IContractsService
    {
        Task<Result<List<ContractResponseDto>>> GetAllContracts();
        Task<Result<ContractResponseDto>> GetById(long id);
        Task<Result<List<ContractResponseDto>>> GetByShopRenterId(long shopRenterId);
        Task<Result<long>> Create(ContractRequestDto dto);
        Task<Result<ContractResponseDto>> Update(ContractRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
