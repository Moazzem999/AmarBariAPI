using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Entities.Context;
using AmarBariAPI.Entities.Shop;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Shared.Enum;
using AmarBariAPI.Shared.Infrastructure;
using AmarBariAPI.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace AmarBariAPI.Repositories
{
    public class ShopsRepository(AppDbContext context
        , ICurrentUserService currentUserService) : IShopsRepository
    {
        private readonly AppDbContext context = context;
        private readonly ICurrentUserService currentUserService = currentUserService;

        public async Task<Result<long>> Create(ShopRequestDto dto)
        {
            if (dto == null)
                return await Result<long>.BadRequestAsync("Invalid request.");

            if (string.IsNullOrWhiteSpace(dto.ShopNumber))
                return await Result<long>.BadRequestAsync("Shop number is required.");

            var userId = currentUserService.UserId;

            // Check duplicate ShopNumber for same user
            var isExists = await context.Shops.AsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.ShopNumber == dto.ShopNumber);

            if (isExists)
            {
                return await Result<long>.BadRequestAsync("This shop number already exists for your account.");
            }

            var newEntity = new ShopEntity
            {
                Name = dto.Name,
                MarketName = dto.MarketName,
                ShopNumber = dto.ShopNumber,
                CurrentRent = dto.CurrentRent,
                UserId = userId
            };

            await context.Shops.AddAsync(newEntity);
            await context.SaveChangesAsync();

            return await Result<long>.SuccessAsync($"Shop successfully created", newEntity.Id);
        }

        public async Task<Result<List<ShopResponseDto>>> GetAllShops()
        {
            var userId = currentUserService.UserId;
            var data = await context.Shops
                .AsNoTracking()
                .Where(x => x.Status == Status.Active && x.UserId == userId && x.CreatedBy == userId)
                .Select(x => new ShopResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    MarketName = x.MarketName,
                    ShopNumber = x.ShopNumber,
                    CurrentRent = x.CurrentRent,
                    OwnerId = x.UserId,
                    OwnerName = x.User.Name,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<ShopResponseDto>>.SuccessAsync("", data);
        }
    }
}
