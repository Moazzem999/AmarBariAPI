using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Entities.Context;
using AmarBariAPI.Entities.Home;
using AmarBariAPI.Repositories.Interfaces;
using AmarBariAPI.Shared.Enum;
using AmarBariAPI.Shared.Infrastructure;
using AmarBariAPI.Shared.Services;
using AmarBariAPI.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace AmarBariAPI.Repositories
{
    public class FlatRenterRepository(AppDbContext context, 
        ICurrentUserService currentUserService, 
        IWebHostEnvironment environment) : IFlatRenterRepository
    {
        private readonly AppDbContext context = context;
        private readonly ICurrentUserService currentUserService = currentUserService;
        private readonly IWebHostEnvironment environment = environment;

        public async Task<Result<List<FlatRenterResponseDto>>> GetAllFlatRenters()
        {
            var userId = currentUserService.UserId;
            var data = await context.FlatRenters
                .AsNoTracking()
                .Where(x => x.Status == Status.Active && x.CreatedBy == userId)
                .Select(x => new FlatRenterResponseDto
                {
                    Id = x.Id,
                    FlatId = x.FlatId,
                    FlatName = x.Flat.Name,
                    Name = x.Name,
                    FatherName = x.FatherName,
                    NidNo = x.NidNo,
                    DateOfBirth = x.DateOfBirth,
                    MaritalStatus = x.MaritalStatus,
                    Religion = x.Religion,
                    Occupation = x.Occupation,
                    AcademicQualification = x.AcademicQualification,
                    Mobile = x.Mobile,
                    PresentAddress = x.PresentAddress,
                    PermanentAddress = x.PermanentAddress,
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

            return await Result<List<FlatRenterResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<FlatRenterResponseDto>> GetById(long id)
        {
            var userId = currentUserService.UserId;
            var data = await context.FlatRenters
                .AsNoTracking()
                .Where(x => x.Id == id && x.Status == Status.Active && x.CreatedBy == userId)
                .Select(x => new FlatRenterResponseDto
                {
                    Id = x.Id,
                    FlatId = x.FlatId,
                    FlatName = x.Flat.Name,
                    Name = x.Name,
                    FatherName = x.FatherName,
                    NidNo = x.NidNo,
                    DateOfBirth = x.DateOfBirth,
                    MaritalStatus = x.MaritalStatus,
                    Religion = x.Religion,
                    Occupation = x.Occupation,
                    AcademicQualification = x.AcademicQualification,
                    Mobile = x.Mobile,
                    PresentAddress = x.PresentAddress,
                    PermanentAddress = x.PermanentAddress,
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
                return await Result<FlatRenterResponseDto>.RecordNotFoundAsync("Flat Renter not found.");

            return await Result<FlatRenterResponseDto>.SuccessAsync("", data);
        }

        public async Task<Result<List<FlatRenterResponseDto>>> GetByFlatId(long flatId)
        {
            var userId = currentUserService.UserId;
            var data = await context.FlatRenters
                .AsNoTracking()
                .Where(x => x.FlatId == flatId && x.Status == Status.Active && x.CreatedBy == userId)
                .Select(x => new FlatRenterResponseDto
                {
                    Id = x.Id,
                    FlatId = x.FlatId,
                    FlatName = x.Flat.Name,
                    Name = x.Name,
                    FatherName = x.FatherName,
                    NidNo = x.NidNo,
                    DateOfBirth = x.DateOfBirth,
                    MaritalStatus = x.MaritalStatus,
                    Religion = x.Religion,
                    Occupation = x.Occupation,
                    AcademicQualification = x.AcademicQualification,
                    Mobile = x.Mobile,
                    PresentAddress = x.PresentAddress,
                    PermanentAddress = x.PermanentAddress,
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

            return await Result<List<FlatRenterResponseDto>>.SuccessAsync("", data);
        }

        public async Task<Result<long>> Create(FlatRenterRequestDto dto)
        {
            var maxFileSize = 300 * 1024; // 300 KB
            string[] allowedFileTypes = [".jpg", ".jpeg", ".png"];

            if (dto is null)
                return await Result<long>.BadRequestAsync("Invalid request.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return await Result<long>.BadRequestAsync("Renter name is required.");

            var userId = currentUserService.UserId;
            
            var isFlatValid = await context.Flats.AsNoTracking()
                .AnyAsync(x => x.Id == dto.FlatId && x.Status == Status.Active && x.CreatedBy == userId);
            if (!isFlatValid)
                return await Result<long>.BadRequestAsync("The target flat was not found or permission denied.");

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

            var newEntity = new FlatRenterEntity
            {
                FlatId = dto.FlatId,
                Name = dto.Name,
                FatherName = dto.FatherName,
                NidNo = dto.NidNo,
                DateOfBirth = dto.DateOfBirth,
                MaritalStatus = dto.MaritalStatus,
                Religion = dto.Religion,
                Occupation = dto.Occupation,
                AcademicQualification = dto.AcademicQualification,
                Mobile = dto.Mobile,
                PresentAddress = dto.PresentAddress,
                PermanentAddress = dto.PermanentAddress,
                RentDate = dto.RentDate,
                AdvancedPaymet = dto.AdvancedPaymet,
                ImagePath = imagePath
            };

            await context.FlatRenters.AddAsync(newEntity);
            await context.SaveChangesAsync();

            return await Result<long>.SuccessAsync("Flat Renter successfully created.", newEntity.Id);
        }

        public async Task<Result<FlatRenterResponseDto>> Update(FlatRenterRequestDto dto)
        {
            var maxFileSize = 300 * 1024; // 300 KB
            string[] allowedFileTypes = [".jpg", ".jpeg", ".png"];

            if (dto is null)
                return await Result<FlatRenterResponseDto>.BadRequestAsync("Invalid request.");

            var userId = currentUserService.UserId;
            var data = await context.FlatRenters
                .Include(x => x.Flat)
                .Where(x => x.Id == dto.Id && x.Status == Status.Active && x.CreatedBy == userId)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<FlatRenterResponseDto>.RecordNotFoundAsync("Flat Renter not found.");

            if (data.FlatId != dto.FlatId)
            {
                var isFlatValid = await context.Flats.AsNoTracking()
                    .AnyAsync(x => x.Id == dto.FlatId && x.Status == Status.Active && x.CreatedBy == userId);
                if (!isFlatValid)
                    return await Result<FlatRenterResponseDto>.BadRequestAsync("The target flat was not found or permission denied.");

                data.FlatId = dto.FlatId;
            }

            if (dto.Image != null)
            {
                var fileType = Path.GetExtension(dto.Image.FileName).ToLower();

                if (dto.Image.Length > maxFileSize)
                    return await Result<FlatRenterResponseDto>.BadRequestAsync("Image size must be less than 300 KB.");

                if (string.IsNullOrEmpty(fileType) || !allowedFileTypes.Contains(fileType))
                    return await Result<FlatRenterResponseDto>.BadRequestAsync("Invalid file type. Allowed types: .jpg, .jpeg, .png");

                if (!string.IsNullOrEmpty(data.ImagePath))
                {
                    DeleteImage(data.ImagePath);
                }
                data.ImagePath = await SaveImage(dto.Image);
            }

            data.Name = dto.Name;
            data.FatherName = dto.FatherName;
            data.NidNo = dto.NidNo;
            data.DateOfBirth = dto.DateOfBirth;
            data.MaritalStatus = dto.MaritalStatus;
            data.Religion = dto.Religion;
            data.Occupation = dto.Occupation;
            data.AcademicQualification = dto.AcademicQualification;
            data.Mobile = dto.Mobile;
            data.PresentAddress = dto.PresentAddress;
            data.PermanentAddress = dto.PermanentAddress;
            data.RentDate = dto.RentDate;
            data.AdvancedPaymet = dto.AdvancedPaymet;

            await context.SaveChangesAsync();

            var response = new FlatRenterResponseDto
            {
                Id = data.Id,
                FlatId = data.FlatId,
                FlatName = data.Flat.Name,
                Name = data.Name,
                FatherName = data.FatherName,
                NidNo = data.NidNo,
                DateOfBirth = data.DateOfBirth,
                MaritalStatus = data.MaritalStatus,
                Religion = data.Religion,
                Occupation = data.Occupation,
                AcademicQualification = data.AcademicQualification,
                Mobile = data.Mobile,
                PresentAddress = data.PresentAddress,
                PermanentAddress = data.PermanentAddress,
                RentDate = data.RentDate,
                AdvancedPaymet = data.AdvancedPaymet,
                ImagePath = data.ImagePath,
                CreatedOn = data.CreatedOn,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                UpdatedBy = data.UpdatedBy,
                Status = data.Status
            };

            return await Result<FlatRenterResponseDto>.SuccessAsync("Flat Renter successfully updated.", response);
        }

        public async Task<Result<string>> Delete(long id)
        {
            var userId = currentUserService.UserId;
            var data = await context.FlatRenters
                .Where(x => x.Id == id && x.Status == Status.Active && x.CreatedBy == userId)
                .FirstOrDefaultAsync();

            if (data is null)
                return await Result<string>.RecordNotFoundAsync("Flat Renter not found.");

            data.Status = Status.Deleted;
            await context.SaveChangesAsync();

            return await Result<string>.SuccessAsync("Flat Renter successfully deleted.");
        }

        private async Task<string> SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(environment.ContentRootPath, "wwwroot", "Uploads", "FlatRenters");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Helper.GenerateUniqueFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/Uploads/FlatRenters/{uniqueFileName}";
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
