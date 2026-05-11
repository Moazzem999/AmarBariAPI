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
    public class HomesRepository(AppDbContext context, ICurrentUserService currentUserService) : IHomesRepository
    {
        private readonly AppDbContext context = context;
        private readonly ICurrentUserService currentUserService = currentUserService;

        public async Task<Result<List<HomeResponseDto>>> GetAllHomes()
        {
            var userId = currentUserService.UserId;
            var data = await context.Homes
                .AsNoTracking()
                .Where(x => x.Status == Status.Active && x.UserId == userId)
                .Select(x => new HomeResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UserId = x.UserId,
                    UserName = x.User.Name,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<HomeResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<HomeResponseDto>> GetById(long id)
        {
            var userId = currentUserService.UserId;
            var data = await context.Homes
                .AsNoTracking()
                .Where(x => x.Id == id && x.Status == Status.Active && x.UserId == userId)
                .Select(x => new HomeResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UserId = x.UserId,
                    UserName = x.User.Name,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<HomeResponseDto>.RecordNotFoundAsync("Home not found.");

            return await Result<HomeResponseDto>.SuccessAsync("", data);
        }

        public async Task<Result<long>> Create(HomeRequestDto dto)
        {
            if (dto is null)
                return await Result<long>.BadRequestAsync("Invalid request.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return await Result<long>.BadRequestAsync("Home name is required.");

            var userId = currentUserService.UserId;

            var isExists = await context.Homes.AsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.Name == dto.Name && x.Status == Status.Active);

            if (isExists)
                return await Result<long>.BadRequestAsync("A home with this name already exists in your account.");

            var newEntity = new HomeEntity
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = userId
            };

            await context.Homes.AddAsync(newEntity);
            await context.SaveChangesAsync();

            return await Result<long>.SuccessAsync("Home successfully created", newEntity.Id);
        }

        public async Task<Result<HomeResponseDto>> Update(HomeRequestDto dto)
        {
            if (dto is null)
                return await Result<HomeResponseDto>.BadRequestAsync("Invalid request.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return await Result<HomeResponseDto>.BadRequestAsync("Home name is required.");

            var userId = currentUserService.UserId;

            var data = await context.Homes.Include(x => x.User)
                .Where(x => x.Id == dto.Id && x.Status == Status.Active && x.UserId == userId)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<HomeResponseDto>.RecordNotFoundAsync("Home not found");

            data.Name = dto.Name;
            data.Description = dto.Description;

            await context.SaveChangesAsync();

            var responseDto = new HomeResponseDto
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                UserId = data.UserId,
                UserName = data.User.Name,
                CreatedOn = data.CreatedOn,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                UpdatedBy = data.UpdatedBy,
                Status = data.Status
            };

            return await Result<HomeResponseDto>.SuccessAsync("Home successfully updated", responseDto);
        }

        public async Task<Result<string>> Delete(long id)
        {
            var userId = currentUserService.UserId;
            var data = await context.Homes
                .Where(x => x.Id == id && x.Status == Status.Active && x.UserId == userId)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<string>.RecordNotFoundAsync("Home not found");

            data.Status = Status.Deleted;

            await context.SaveChangesAsync();
            return await Result<string>.SuccessAsync("Home successfully deleted");
        }
    }
}
