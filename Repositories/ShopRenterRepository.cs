using AmarBariAPI.Dtos.Common;
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
    public class ShopRenterRepository(AppDbContext context,
        ICurrentUserService currentUserService,
        IWebHostEnvironment environment) : IShopRenterRepository
    {
        private readonly AppDbContext context = context;
        private readonly ICurrentUserService currentUserService = currentUserService;
        private readonly IWebHostEnvironment environment = environment;

        public async Task<Result<List<ShopRenterResponseDto>>> GetAllShopRenters()
        {
            var userId = currentUserService.UserId;
            var data = await context.ShopRenters
                .AsNoTracking()
                .Where(x => x.Status == Status.Active && x.CreatedBy == userId)
                .Select(x => new ShopRenterResponseDto
                {
                    Id = x.Id,
                    ShopId = x.ShopId,
                    ShopName = x.Shop.Name,
                    Name = x.Name,
                    FatherName = x.FatherName,
                    DateOfBirth = x.DateOfBirth,
                    MaritalStatus = x.MaritalStatus,
                    Religion = x.Religion,
                    PresentAddress = x.PresentAddress,
                    PermanentAddress = x.PermanentAddress,
                    Occupation = x.Occupation,
                    AcademicQualification = x.AcademicQualification,
                    Mobile = x.Mobile,
                    NidNo = x.NidNo,
                    RentDate = x.RentDate,
                    AdvancedPaymet = x.AdvancedPaymet,
                    ImagePath = x.ImagePath,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<ShopRenterResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<ShopRenterResponseDto>> GetById(long id)
        {
            var data = await context.ShopRenters
                .AsNoTracking()
                .Where(x => x.Id == id && x.Status == Status.Active)
                .Select(x => new ShopRenterResponseDto
                {
                    Id = x.Id,
                    ShopId = x.ShopId,
                    ShopName = x.Shop.Name,
                    Name = x.Name,
                    FatherName = x.FatherName,
                    DateOfBirth = x.DateOfBirth,
                    MaritalStatus = x.MaritalStatus,
                    Religion = x.Religion,
                    PresentAddress = x.PresentAddress,
                    PermanentAddress = x.PermanentAddress,
                    Occupation = x.Occupation,
                    AcademicQualification = x.AcademicQualification,
                    Mobile = x.Mobile,
                    NidNo = x.NidNo,
                    RentDate = x.RentDate,
                    AdvancedPaymet = x.AdvancedPaymet,
                    ImagePath = x.ImagePath,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<ShopRenterResponseDto>.RecordNotFoundAsync("Shop Renter not found.");

            return await Result<ShopRenterResponseDto>.SuccessAsync("", data);
        }

        public async Task<Result<List<ShopRenterResponseDto>>> GetByShopId(long shopId)
        {
            var data = await context.ShopRenters
                .AsNoTracking()
                .Where(x => x.ShopId == shopId && x.Status == Status.Active)
                .Select(x => new ShopRenterResponseDto
                {
                    Id = x.Id,
                    ShopId = x.ShopId,
                    ShopName = x.Shop.Name,
                    Name = x.Name,
                    FatherName = x.FatherName,
                    DateOfBirth = x.DateOfBirth,
                    MaritalStatus = x.MaritalStatus,
                    Religion = x.Religion,
                    PresentAddress = x.PresentAddress,
                    PermanentAddress = x.PermanentAddress,
                    Occupation = x.Occupation,
                    AcademicQualification = x.AcademicQualification,
                    Mobile = x.Mobile,
                    NidNo = x.NidNo,
                    RentDate = x.RentDate,
                    AdvancedPaymet = x.AdvancedPaymet,
                    ImagePath = x.ImagePath,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status
                })
                .ToListAsync();

            return await Result<List<ShopRenterResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<long>> Create(ShopRenterRequestDto dto)
        {
            var maxFileSize = 300 * 1024; // 300 KB
            string[] allowedFileTypes = [".jpg", ".jpeg", ".png"];

            if (dto is null)
                return await Result<long>.BadRequestAsync("Invalid request.");

            string? imagePath = null;
            if (dto.Image != null)
            {
                var fileType = Path.GetExtension(dto.Image.FileName).ToLower();

                if (dto.Image.Length > maxFileSize)
                    return await Result<long>.BadRequestAsync("Image size must be less than 300 KB.");

                if (string.IsNullOrEmpty(fileType) || !allowedFileTypes.Contains(fileType))
                    return await Result<long>.BadRequestAsync("Invalid file type. Allowed types: .jpg, .jpeg, .png");

                imagePath = await SaveImage(dto.Image);
            }

            var newEntity = new ShopRenterEntity
            {
                ShopId = dto.ShopId,
                Name = dto.Name,
                FatherName = dto.FatherName,
                DateOfBirth = dto.DateOfBirth,
                MaritalStatus = dto.MaritalStatus,
                Religion = dto.Religion,
                PresentAddress = dto.PresentAddress,
                PermanentAddress = dto.PermanentAddress,
                Occupation = dto.Occupation,
                AcademicQualification = dto.AcademicQualification,
                Mobile = dto.Mobile,
                NidNo = dto.NidNo,
                RentDate = dto.RentDate,
                AdvancedPaymet = dto.AdvancedPaymet,
                ImagePath = imagePath
            };

            await context.ShopRenters.AddAsync(newEntity);
            await context.SaveChangesAsync();

            return await Result<long>.SuccessAsync("Shop Renter successfully created.", newEntity.Id);
        }

        public async Task<Result<ShopRenterResponseDto>> Update(ShopRenterRequestDto dto)
        {
            var maxFileSize = 300 * 1024; // 300 KB
            string[] allowedFileTypes = [".jpg", ".jpeg", ".png"];

            if (dto is null)
                return await Result<ShopRenterResponseDto>.BadRequestAsync("Invalid request.");

            var data = await context.ShopRenters
                .Include(x => x.Shop)
                .Where(x => x.Id == dto.Id && x.Status == Status.Active)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<ShopRenterResponseDto>.RecordNotFoundAsync("Shop Renter not found.");


            if (dto.Image != null)
            {
                var fileType = Path.GetExtension(dto.Image.FileName).ToLower();

                if (dto.Image.Length > maxFileSize)
                    return await Result<ShopRenterResponseDto>.BadRequestAsync("Image size must be less than 300 KB.");

                if (string.IsNullOrEmpty(fileType) || !allowedFileTypes.Contains(fileType))
                    return await Result<ShopRenterResponseDto>.BadRequestAsync("Invalid file type. Allowed types: .jpg, .jpeg, .png");

                if (!string.IsNullOrEmpty(data.ImagePath))
                {
                    DeleteImage(data.ImagePath);
                }
                data.ImagePath = await SaveImage(dto.Image);
            }

            data.ShopId = dto.ShopId;
            data.Name = dto.Name;
            data.FatherName = dto.FatherName;
            data.DateOfBirth = dto.DateOfBirth;
            data.MaritalStatus = dto.MaritalStatus;
            data.Religion = dto.Religion;
            data.PresentAddress = dto.PresentAddress;
            data.PermanentAddress = dto.PermanentAddress;
            data.Occupation = dto.Occupation;
            data.AcademicQualification = dto.AcademicQualification;
            data.Mobile = dto.Mobile;
            data.NidNo = dto.NidNo;
            data.RentDate = dto.RentDate;
            data.AdvancedPaymet = dto.AdvancedPaymet;

            await context.SaveChangesAsync();

            var response = new ShopRenterResponseDto
            {
                Id = data.Id,
                ShopId = data.ShopId,
                ShopName = data.Shop.Name,
                Name = data.Name,
                FatherName = data.FatherName,
                DateOfBirth = data.DateOfBirth,
                MaritalStatus = data.MaritalStatus,
                Religion = data.Religion,
                PresentAddress = data.PresentAddress,
                PermanentAddress = data.PermanentAddress,
                Occupation = data.Occupation,
                AcademicQualification = data.AcademicQualification,
                Mobile = data.Mobile,
                NidNo = data.NidNo,
                RentDate = data.RentDate,
                AdvancedPaymet = data.AdvancedPaymet,
                ImagePath = data.ImagePath,
                CreatedOn = data.CreatedOn,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                UpdatedBy = data.UpdatedBy,
                Status = data.Status
            };

            return await Result<ShopRenterResponseDto>.SuccessAsync("Shop Renter successfully updated.", response);
        }

        public async Task<Result<string>> Delete(long id)
        {
            var data = await context.ShopRenters
                .Where(x => x.Id == id && x.Status == Status.Active)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<string>.RecordNotFoundAsync("Shop Renter not found.");

            data.Status = Status.Deleted;
            await context.SaveChangesAsync();

            return await Result<string>.SuccessAsync("Shop Renter successfully deleted.");
        }

        public async Task<Result<List<DropdownDto>>> GetAllMaritalStatus()
        {
            var data = Enum.GetValues<MaritalStatus>()
                .Select(x => new DropdownDto
                {
                    Id = (int)x,
                    Name = x.GetEnumDescription()
                })
                .ToList();

            return await Result<List<DropdownDto>>.SuccessAsync("", data);
        }

        public async Task<Result<List<DropdownDto>>> GetAllReligion()
        {
            var data = Enum.GetValues<Religion>()
                .Select(x => new DropdownDto
                {
                    Id = (int)x,
                    Name = x.GetEnumDescription()
                })
                .ToList();

            return await Result<List<DropdownDto>>.SuccessAsync("", data);
        }

        private async Task<string> SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(environment.ContentRootPath, "wwwroot", "Uploads", "ShopRenters");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Helper.GenerateUniqueFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/Uploads/ShopRenters/{uniqueFileName}";
        }

        private void DeleteImage(string imagePath)
        {
            var filePath = Path.Combine(environment.ContentRootPath, "wwwroot", imagePath.TrimStart('/'));
            if (File.Exists(filePath))
            {
                try { File.Delete(filePath); } catch { /* log error */ }
            }
        }
    }
}
