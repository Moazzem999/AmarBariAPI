using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Entities.Context;
using AmarBariAPI.Entities.Shop;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Shared.Enum;
using AmarBariAPI.Shared.Infrastructure;
using AmarBariAPI.Shared.Services;
using AmarBariAPI.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace AmarBariAPI.Repositories
{
    public class ContractsRepository(AppDbContext context,
        ICurrentUserService currentUserService,
        IWebHostEnvironment environment) : IContractsRepository
    {
        private readonly AppDbContext context = context;
        private readonly ICurrentUserService currentUserService = currentUserService;
        private readonly IWebHostEnvironment environment = environment;

        public async Task<Result<List<ContractResponseDto>>> GetAllContracts()
        {
            var userId = currentUserService.UserId;
            var data = await context.Contracts
                .AsNoTracking()
                .Where(x => x.CreatedBy == userId)
                .Select(x => new ContractResponseDto
                {
                    Id = x.Id,
                    ShopRenterId = x.ShopRenterId,
                    ShopRenterName = x.ShopRenter.Name,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    FilePath = x.FilePath,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<ContractResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<ContractResponseDto>> GetById(long id)
        {
            var userId = currentUserService.UserId;
            var data = await context.Contracts
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new ContractResponseDto
                {
                    Id = x.Id,
                    ShopRenterId = x.ShopRenterId,
                    ShopRenterName = x.ShopRenter.Name,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    FilePath = x.FilePath,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<ContractResponseDto>.RecordNotFoundAsync("Contract not found.");

            return await Result<ContractResponseDto>.SuccessAsync("", data);
        }

        public async Task<Result<List<ContractResponseDto>>> GetByShopRenterId(long shopRenterId)
        {
            var data = await context.Contracts
                .AsNoTracking()
                .Where(x => x.ShopRenterId == shopRenterId)
                .Select(x => new ContractResponseDto
                {
                    Id = x.Id,
                    ShopRenterId = x.ShopRenterId,
                    ShopRenterName = x.ShopRenter.Name,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    FilePath = x.FilePath,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<ContractResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<long>> Create(ContractRequestDto dto)
        {
            var maxFileSize = 2 * 1024 * 1024; // 2MB
            string[] allowedFileTypes = [".jpg", ".jpeg", ".png", ".doc", ".docx",".pdf"];

            if (dto is null)
                return await Result<long>.BadRequestAsync("Invalid request.");

            if (dto.ShopRenterId == null || dto.ShopRenterId == 0)
                return await Result<long>.BadRequestAsync("Shop Renter is required.");

            if (!dto.StartDate.HasValue)
                return await Result<long>.BadRequestAsync("Start Date is required.");

            if (!dto.EndDate.HasValue)
                return await Result<long>.BadRequestAsync("End Date is required.");

            if (dto.EndDate < dto.StartDate)
                return await Result<long>.BadRequestAsync("End Date cannot be before Start Date.");


            // Verify Shop Renter exists and is active
            var renterExists = await context.ShopRenters
                .AnyAsync(x => x.Id == dto.ShopRenterId && x.Status == Status.Active);

            if (!renterExists)
                return await Result<long>.BadRequestAsync("Invalid or inactive Shop Renter.");

            string? filePath = null;
            if (dto.File != null)
            {
                var fileType = Path.GetExtension(dto.File.FileName).ToLower();

                if (dto.File.Length > maxFileSize)
                    return await Result<long>.BadRequestAsync("File size must be less than 2 MB.");

                if (string.IsNullOrEmpty(fileType) || !allowedFileTypes.Contains(fileType))
                    return await Result<long>.BadRequestAsync("Invalid file type. Allowed types: .jpg, .jpeg, .png, .doc, .docx, .pdf");

                filePath = await SaveFile(dto.File);
            }

            var newEntity = new ContractEntity
            {
                ShopRenterId = dto.ShopRenterId.Value,
                Description = dto.Description ?? string.Empty,
                StartDate = dto.StartDate.Value,
                EndDate = dto.EndDate.Value,
                FilePath = filePath
            };

            await context.Contracts.AddAsync(newEntity);
            await context.SaveChangesAsync();

            return await Result<long>.SuccessAsync("Contract successfully created.", newEntity.Id);
        }

        public async Task<Result<ContractResponseDto>> Update(ContractRequestDto dto)
        {
            var maxFileSize = 2 * 1024 * 1024; // 2MB
            string[] allowedFileTypes = [".jpg", ".jpeg", ".png", ".doc", ".docx", ".pdf"];

            if (dto is null)
                return await Result<ContractResponseDto>.BadRequestAsync("Invalid request.");

            var data = await context.Contracts
                .Include(x => x.ShopRenter)
                .Where(x => x.Id == dto.Id && x.Status == Status.Active)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<ContractResponseDto>.RecordNotFoundAsync("Contract not found.");

            if (dto.File != null)
            {
                var fileType = Path.GetExtension(dto.File.FileName).ToLower();

                if (dto.File.Length > maxFileSize)
                    return await Result<ContractResponseDto>.BadRequestAsync("File size must be less than 2 MB.");

                if (string.IsNullOrEmpty(fileType) || !allowedFileTypes.Contains(fileType))
                    return await Result<ContractResponseDto>.BadRequestAsync("Invalid file type. Allowed types: .jpg, .jpeg, .png, .doc, .docx, .pdf");

                if (!string.IsNullOrEmpty(data.FilePath))
                {
                    DeleteFile(data.FilePath);
                }
                data.FilePath = await SaveFile(dto.File);
            }

            if (dto.ShopRenterId.HasValue && dto.ShopRenterId != 0)
            {
                var renterExists = await context.ShopRenters
                    .AnyAsync(x => x.Id == dto.ShopRenterId && x.Status == Status.Active);
                if (!renterExists)
                    return await Result<ContractResponseDto>.BadRequestAsync("Invalid or inactive Shop Renter.");

                data.ShopRenterId = dto.ShopRenterId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description)) data.Description = dto.Description;
            
            // Handle date validation for updates
            var tempStartDate = dto.StartDate ?? data.StartDate;
            var tempEndDate = dto.EndDate ?? data.EndDate;
            if (tempEndDate < tempStartDate)
                return await Result<ContractResponseDto>.BadRequestAsync("End Date cannot be before Start Date.");

            if (dto.StartDate.HasValue) data.StartDate = dto.StartDate.Value;
            if (dto.EndDate.HasValue) data.EndDate = dto.EndDate.Value;

            await context.SaveChangesAsync();

            var response = new ContractResponseDto
            {
                Id = data.Id,
                ShopRenterId = data.ShopRenterId,
                ShopRenterName = data.ShopRenter.Name,
                Description = data.Description,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                FilePath = data.FilePath,
                CreatedOn = data.CreatedOn,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                UpdatedBy = data.UpdatedBy,
                Status = data.Status
            };

            return await Result<ContractResponseDto>.SuccessAsync("Contract successfully updated.", response);
        }

        public async Task<Result<string>> Delete(long id)
        {
            var data = await context.Contracts
                .Where(x => x.Id == id && x.Status == Status.Active)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<string>.RecordNotFoundAsync("Contract not found.");

            data.Status = Status.Deleted;
            await context.SaveChangesAsync();

            return await Result<string>.SuccessAsync("Contract successfully deleted.");
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(environment.ContentRootPath, "wwwroot", "Uploads", "Contracts");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Helper.GenerateUniqueFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/Uploads/Contracts/{uniqueFileName}";
        }

        private void DeleteFile(string filePath)
        {
            var absolutePath = Path.Combine(environment.ContentRootPath, "wwwroot", filePath.TrimStart('/'));
            if (File.Exists(absolutePath))
            {
                try { File.Delete(absolutePath); } catch { /* log error */ }
            }
        }
    }
}
