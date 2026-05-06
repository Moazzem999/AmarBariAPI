using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Repositories.Interfaces
{
    public interface IContractsRepository
    {
        Task<Result<List<ContractResponseDto>>> GetAllContracts();
        Task<Result<ContractResponseDto>> GetById(long id);
        Task<Result<long>> Create(ContractRequestDto dto);
        Task<Result<ContractResponseDto>> Update(ContractRequestDto dto);
        Task<Result<string>> Delete(long id);
    }
}
