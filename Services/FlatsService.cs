using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Services.Interfaces;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services
{
    public class FlatsService(IFlatsRepository flatsRepository) : IFlatsService
    {
        private readonly IFlatsRepository flatsRepository = flatsRepository;

        public async Task<Result<List<FlatResponseDto>>> GetAllFlats()
        {
            return await flatsRepository.GetAllFlats();
        }

        public async Task<Result<FlatResponseDto>> GetById(long id)
        {
            return await flatsRepository.GetById(id);
        }

        public async Task<Result<List<FlatResponseDto>>> GetByHomeId(long homeId)
        {
            return await flatsRepository.GetByHomeId(homeId);
        }

        public async Task<Result<long>> Create(FlatRequestDto dto)
        {
            return await flatsRepository.Create(dto);
        }

        public async Task<Result<FlatResponseDto>> Update(FlatRequestDto dto)
        {
            return await flatsRepository.Update(dto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            return await flatsRepository.Delete(id);
        }
    }
}
