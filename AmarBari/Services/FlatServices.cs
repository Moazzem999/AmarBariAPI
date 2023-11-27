using AmarBari.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmarBari.Services
{
    public class FlatServices : IFlatServices
    {
        private readonly AmarBariDbContext _context;
        public FlatServices(AmarBariDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAllFlats()
        {
            var flatList = new List<object>();
            var data = await _context.Flats.ToListAsync();

            foreach (var item in data)
            {
                flatList.Add(new
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Floor = item.Floor
                });
            }
            return flatList;
        }
    }

    public interface IFlatServices
    {
        Task<object> GetAllFlats();
    }
}
