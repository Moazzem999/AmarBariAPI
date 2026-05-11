using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Services.Interfaces;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services
{
    public class HomesService(IHomesRepository homesRepository) : IHomesService
    {
        private readonly IHomesRepository homesRepository = homesRepository;

        public async Task<Result<List<HomeResponseDto>>> GetAllHomes()
        {
            return await homesRepository.GetAllHomes();
        }

        public async Task<Result<HomeResponseDto>> GetById(long id)
        {
            return await homesRepository.GetById(id);
        }

        public async Task<Result<long>> Create(HomeRequestDto dto)
        {
            return await homesRepository.Create(dto);
        }

        public async Task<Result<HomeResponseDto>> Update(HomeRequestDto dto)
        {
            return await homesRepository.Update(dto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            return await homesRepository.Delete(id);
        }
    }
}
