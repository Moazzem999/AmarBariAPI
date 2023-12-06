using AmarBari.Dto;
using AmarBari.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmarBari.Services
{
    public class UserServices : IUserServices
    {
        private readonly AmarBariDbContext _context;
        public UserServices(AmarBariDbContext context)
        {
            _context = context;
        }
        public async Task<List<object>> GetUsers()
        {
            var userList = new List<object>();
            var data = await _context.Users
                .Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToListAsync();

            foreach (var item in data)
            {
                userList.Add(new
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

        public async Task<object> GetUser(long id)
        {
            var data = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
            if (data != null)
            {
                return new
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
            return data;
        }

        public async Task<object> AddUser(UserDto user)
        {
            var newUser = new User
            {
                UserType = user.UserType,
                Name = user.Name,
                Email = user.Email,
                NidNo = user.NidNo,
                ImageUrl = user.ImageUrl, //=========todo========
                NidImageUrl = user.NidImageUrl, //=========todo========
                LoginId = user.NidNo,
                Password = user.NidNo,
                IsApproved = user.UserType == "Owner" ? false : true,
                RenterId = user.RenterId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<object> UpdateUser(UserDto user)
        {
            var data = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id && x.IsActive == true);

            if (data != null)
            {
                data.Name = user.Name;
                data.Email = user.Email;
                data.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return data;
            }

            return null;
        }

        public async Task<bool> DeleteUser(long id)
        {
            var data = await _context.Users.FindAsync(id);
            if (data != null)
            {
                data.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CheckDuplicateUser(string nidNo)
        {
            var checkDuplicateUser = await _context.Users.FirstOrDefaultAsync(x => x.NidNo == nidNo && x.IsActive == true);
            bool returnType = checkDuplicateUser != null;
            return returnType;
        }
    }

    public interface IUserServices
    {
        Task<List<object>> GetUsers();
        Task<object> GetUser(long id);
        Task<object> AddUser(UserDto user);
        Task<object> UpdateUser(UserDto user);
        Task<bool> DeleteUser(long id);
        Task<bool> CheckDuplicateUser(string nidNo);
    }
}
