using AmarBari.Dto;
using AmarBari.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;

namespace AmarBari.Services
{
    public class UserServices : IUserServices
    {
        private readonly AmarBariDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserServices(AmarBariDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<List<UserResponseDto>> GetUsers()
        {
            var userList = new List<UserResponseDto>();
            var data = await context.Users
                .Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToListAsync();

            foreach (var item in data)
            {
                userList.Add(new UserResponseDto
                {
                    Id = item.Id,
                    UserType = item.UserType,
                    Name = item.Name,
                    Email = item.Email,
                    NidNo = item.NidNo,
                    ImageUrl = item.ImageUrl,
                    NidImageUrl = item.NidImageUrl,
                    LoginId = item.LoginId,
                    IsApproved = item.IsApproved
                });
            }
            return userList;
        }

        public async Task<UserResponseDto> GetUser(long id)
        {
            var data = await context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
            if (data == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                Id = data.Id,
                UserType = data.UserType,
                Name = data.Name,
                Email = data.Email,
                NidNo = data.NidNo,
                ImageUrl = data.ImageUrl,
                NidImageUrl = data.NidImageUrl,
                LoginId = data.LoginId,
                IsApproved = data.IsApproved
            };
        }

        public async Task<User> AddUser(UserRequestDto user)
        {
            string imageUrl = "\\Images\\" + user.NidNo + "_Image_" + user.Image.FileName;
            string nidImageUrl = "\\Images\\" + user.NidNo + "_NidImage_" + user.NidImage.FileName;

            if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\Images\\"))
            {
                Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\Images\\");
            }

            using (FileStream imageStream = File.Create(webHostEnvironment.WebRootPath + imageUrl))
            {
                await user.Image.CopyToAsync(imageStream);
                imageStream.Flush();
            }

            using (FileStream nidImageStream = File.Create(webHostEnvironment.WebRootPath + nidImageUrl))
            {
                await user.NidImage.CopyToAsync(nidImageStream);
                nidImageStream.Flush();
            }


            var newUser = new User
            {
                UserType = "Owner",
                Name = user.Name,
                Email = user.Email,
                NidNo = user.NidNo,
                ImageUrl = imageUrl,
                NidImageUrl = nidImageUrl,
                LoginId = user.NidNo,
                Password = user.Password,
                IsApproved = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true
            };

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> UpdateUser(UserRequestDto user)
        {
            var data = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id && x.IsActive == true);

            if (data == null)
            {
                return null;
            }

            if (user.Image != null)
            {
                string imageUrl = "\\Images\\" + user.NidNo + "_Image_" + user.Image.FileName;
                File.Delete(webHostEnvironment.WebRootPath + data.ImageUrl);

                using (FileStream imageStream = File.Create(webHostEnvironment.WebRootPath + imageUrl))
                {
                    await user.Image.CopyToAsync(imageStream);
                    imageStream.Flush();
                }
            }

            if (user.NidImage != null)
            {
                string nidImageUrl = "\\Images\\" + user.NidNo + "_NidImage_" + user.NidImage.FileName;
                File.Delete(webHostEnvironment.WebRootPath + data.NidImageUrl);

                using (FileStream nidImageStream = File.Create(webHostEnvironment.WebRootPath + nidImageUrl))
                {
                    await user.NidImage.CopyToAsync(nidImageStream);
                    nidImageStream.Flush();
                }
            }

            data.Name = user.Name;
            data.Email = user.Email;
            data.NidNo = user.NidNo;
            data.LoginId = user.NidNo;
            data.Password = user.Password == null ? data.Password : user.Password;
            data.ImageUrl = user.Image == null ? data.ImageUrl : "\\Images\\" + user.NidNo + "_Image_" + user.Image.FileName;
            data.NidImageUrl = user.NidImage == null ? data.NidImageUrl : "\\Images\\" + user.NidNo + "_NidImage_" + user.NidImage.FileName;
            data.UpdatedDate = DateTime.Now;

            await context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteUser(long id)
        {
            var data = await context.Users.FindAsync(id);
            if (data != null)
            {
                data.IsActive = false;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CheckDuplicateUser(string nidNo)
        {
            var checkDuplicateUser = await context.Users.FirstOrDefaultAsync(x => x.NidNo == nidNo && x.IsActive == true);
            bool returnType = checkDuplicateUser != null;
            return returnType;
        }
    }

    public interface IUserServices
    {
        Task<List<UserResponseDto>> GetUsers();
        Task<UserResponseDto> GetUser(long id);
        Task<User> AddUser(UserRequestDto user);
        Task<User> UpdateUser(UserRequestDto user);
        Task<bool> DeleteUser(long id);
        Task<bool> CheckDuplicateUser(string nidNo);
    }
}
