using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Entities.Context;
using AmarBariAPI.Entities.Home;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Shared.Enum;
using AmarBariAPI.Shared.Infrastructure;
using AmarBariAPI.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace AmarBariAPI.Repositories
{
    public class FlatsRepository(AppDbContext context, ICurrentUserService currentUserService) : IFlatsRepository
    {
        private readonly AppDbContext context = context;
        private readonly ICurrentUserService currentUserService = currentUserService;

        public async Task<Result<List<FlatResponseDto>>> GetAllFlats()
        {
            var userId = currentUserService.UserId;
            var data = await context.Flats
                .AsNoTracking()
                .Where(x => x.Status == Status.Active && x.CreatedBy == userId)
                .Select(x => new FlatResponseDto
                {
                    Id = x.Id,
                    HomeId = x.HomeId,
                    HomeName = x.Home.Name,
                    Name = x.Name,
                    Description = x.Description,
                    Floor = x.Floor,
                    CurrentRent = x.CurrentRent,
                    GasBill = x.GasBill,
                    WaterBill = x.WaterBill,
                    ServiceCharge = x.ServiceCharge,
                    OthersBill = x.OthersBill,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<FlatResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<FlatResponseDto>> GetById(long id)
        {
            var userId = currentUserService.UserId;
            var data = await context.Flats
                .AsNoTracking()
                .Where(x => x.Id == id && x.Status == Status.Active && x.CreatedBy == userId)
                .Select(x => new FlatResponseDto
                {
                    Id = x.Id,
                    HomeId = x.HomeId,
                    HomeName = x.Home.Name,
                    Name = x.Name,
                    Description = x.Description,
                    Floor = x.Floor,
                    CurrentRent = x.CurrentRent,
                    GasBill = x.GasBill,
                    WaterBill = x.WaterBill,
                    ServiceCharge = x.ServiceCharge,
                    OthersBill = x.OthersBill,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<FlatResponseDto>.RecordNotFoundAsync("Flat not found.");

            return await Result<FlatResponseDto>.SuccessAsync("", data);
        }

        public async Task<Result<List<FlatResponseDto>>> GetByHomeId(long homeId)
        {
            var userId = currentUserService.UserId;
            var data = await context.Flats
                .AsNoTracking()
                .Where(x => x.HomeId == homeId && x.Status == Status.Active && x.CreatedBy == userId)
                .Select(x => new FlatResponseDto
                {
                    Id = x.Id,
                    HomeId = x.HomeId,
                    HomeName = x.Home.Name,
                    Name = x.Name,
                    Description = x.Description,
                    Floor = x.Floor,
                    CurrentRent = x.CurrentRent,
                    GasBill = x.GasBill,
                    WaterBill = x.WaterBill,
                    ServiceCharge = x.ServiceCharge,
                    OthersBill = x.OthersBill,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<FlatResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<long>> Create(FlatRequestDto dto)
        {
            if (dto is null)
                return await Result<long>.BadRequestAsync("Invalid request.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return await Result<long>.BadRequestAsync("Flat name is required.");

            var userId = currentUserService.UserId;

            var isHomeExists = await context.Homes.AsNoTracking()
                .AnyAsync(x => x.Id == dto.HomeId && x.Status == Status.Active && x.UserId == userId);

            if (!isHomeExists)
                return await Result<long>.BadRequestAsync("Selected home does not exist or you don't have permission.");

            var isExists = await context.Flats.AsNoTracking()
                .AnyAsync(x => x.HomeId == dto.HomeId && x.Name == dto.Name && x.Status == Status.Active);

            if (isExists)
                return await Result<long>.BadRequestAsync("A flat with this name already exists in this home.");

            var newEntity = new FlatEntity
            {
                HomeId = dto.HomeId,
                Name = dto.Name,
                Description = dto.Description,
                Floor = dto.Floor,
                CurrentRent = dto.CurrentRent,
                GasBill = dto.GasBill,
                WaterBill = dto.WaterBill,
                ServiceCharge = dto.ServiceCharge,
                OthersBill = dto.OthersBill
            };

            await context.Flats.AddAsync(newEntity);
            await context.SaveChangesAsync();

            return await Result<long>.SuccessAsync("Flat successfully created", newEntity.Id);
        }

        public async Task<Result<FlatResponseDto>> Update(FlatRequestDto dto)
        {
            if (dto is null)
                return await Result<FlatResponseDto>.BadRequestAsync("Invalid request.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return await Result<FlatResponseDto>.BadRequestAsync("Flat name is required.");

            var userId = currentUserService.UserId;

            var data = await context.Flats.Include(x => x.Home)
                .Where(x => x.Id == dto.Id && x.Status == Status.Active && x.CreatedBy == userId)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<FlatResponseDto>.RecordNotFoundAsync("Flat not found");

            if (data.HomeId != dto.HomeId)
            {
                var isHomeExists = await context.Homes.AsNoTracking()
                    .AnyAsync(x => x.Id == dto.HomeId && x.Status == Status.Active && x.UserId == userId);

                if (!isHomeExists)
                    return await Result<FlatResponseDto>.BadRequestAsync("Selected home does not exist or permission denied.");

                data.HomeId = dto.HomeId;
            }

            data.Name = dto.Name;
            data.Description = dto.Description;
            data.Floor = dto.Floor;
            data.CurrentRent = dto.CurrentRent;
            data.GasBill = dto.GasBill;
            data.WaterBill = dto.WaterBill;
            data.ServiceCharge = dto.ServiceCharge;
            data.OthersBill = dto.OthersBill;

            await context.SaveChangesAsync();

            var responseDto = new FlatResponseDto
            {
                Id = data.Id,
                HomeId = data.HomeId,
                HomeName = data.Home.Name,
                Name = data.Name,
                Description = data.Description,
                Floor = data.Floor,
                CurrentRent = data.CurrentRent,
                GasBill = data.GasBill,
                WaterBill = data.WaterBill,
                ServiceCharge = data.ServiceCharge,
                OthersBill = data.OthersBill,
                CreatedOn = data.CreatedOn,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                UpdatedBy = data.UpdatedBy,
                Status = data.Status
            };

            return await Result<FlatResponseDto>.SuccessAsync("Flat successfully updated", responseDto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            var userId = currentUserService.UserId;
            var data = await context.Flats
                .Where(x => x.Id == id && x.Status == Status.Active && x.CreatedBy == userId)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<string>.RecordNotFoundAsync("Flat not found");

            data.Status = Status.Deleted;

            await context.SaveChangesAsync();
            return await Result<string>.SuccessAsync("Flat successfully deleted");
        }
    }
}
