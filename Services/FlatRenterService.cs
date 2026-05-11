using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Services.Interfaces;
using AmarBariAPI.Shared.Infrastructure;

namespace AmarBariAPI.Services
{
    public class FlatRenterService(IFlatRenterRepository flatRenterRepository) : IFlatRenterService
    {
        private readonly IFlatRenterRepository flatRenterRepository = flatRenterRepository;

        public async Task<Result<List<FlatRenterResponseDto>>> GetAllFlatRenters()
        {
            return await flatRenterRepository.GetAllFlatRenters();
        }

        public async Task<Result<FlatRenterResponseDto>> GetById(long id)
        {
            return await flatRenterRepository.GetById(id);
        }

        public async Task<Result<List<FlatRenterResponseDto>>> GetByFlatId(long flatId)
        {
            return await flatRenterRepository.GetByFlatId(flatId);
        }

        public async Task<Result<long>> Create(FlatRenterRequestDto dto)
        {
            return await flatRenterRepository.Create(dto);
        }

        public async Task<Result<FlatRenterResponseDto>> Update(FlatRenterRequestDto dto)
        {
            return await flatRenterRepository.Update(dto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            return await flatRenterRepository.Delete(id);
        }
    }
}
