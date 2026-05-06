using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Services.Interfaces;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services
{
    public class ContractsService(IContractsRepository contractsRepository) : IContractsService
    {
        private readonly IContractsRepository contractsRepository = contractsRepository;

        public async Task<Result<List<ContractResponseDto>>> GetAllContracts()
        {
            return await contractsRepository.GetAllContracts();
        }

        public async Task<Result<ContractResponseDto>> GetById(long id)
        {
            return await contractsRepository.GetById(id);
        }

        public async Task<Result<long>> Create(ContractRequestDto dto)
        {
            return await contractsRepository.Create(dto);
        }

        public async Task<Result<ContractResponseDto>> Update(ContractRequestDto dto)
        {
            return await contractsRepository.Update(dto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            return await contractsRepository.Delete(id);
        }
    }
}
