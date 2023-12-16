using AmarBari.Dto;
using AmarBari.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AmarBari.Services
{
    public class FlatServices : IFlatServices
    {
        private readonly AmarBariDbContext context;
        public FlatServices(AmarBariDbContext context)
        {
            this.context = context;
        }

        public async Task<List<object>> GetFlats()
        {
            var flatList = new List<object>();
            var data = await context.Flats.Include(x => x.Building)
                .Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToListAsync();

            foreach (var item in data)
            {
                flatList.Add(new
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Floor = item.Floor,
                    BuildingId = item.BuildingId,
                    BuildingName = item.Building.Name
                });
            }
            return flatList;
        }

        public async Task<object> GetFlat(long id)
        {
            var data = await context.Flats.Include(x => x.Building.User).FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
            if (data != null)
            {
                return new
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    Floor = data.Floor,
                    BuildingId = data.BuildingId,
                    BuildingName = data.Building.Name
                };
            }
            return data;
        }

        public async Task<object> AddFlat(FlatDto flat)
        {
            var newFlat = new Flat
            {
                BuildingId = flat.BuildingId,
                Name = flat.Name,
                Description = flat.Description,
                Floor = flat.Floor,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true
            };

            await context.Flats.AddAsync(newFlat);
            await context.SaveChangesAsync();

            var data = await context.Flats.Include(x => x.Building).FirstOrDefaultAsync(x => x.Id == newFlat.Id);

            return new
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Floor = data.Floor,
                BuildingId = data.BuildingId,
                BuildingName = data.Building.Name
            };
        }

        public async Task<object> UpdateFlat(FlatDto flat)
        {
            var data = await context.Flats.FirstOrDefaultAsync(x => x.Id == flat.Id && x.BuildingId == flat.BuildingId && x.IsActive == true);
            if (data!= null)
            {
                data.Name = flat.Name;
                data.Description = flat.Description;
                data.Floor = flat.Floor;
                data.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
                return data;
            }

            return null;
        }

        public async Task<bool> DeleteFlat(long id)
        {
            var data = await context.Flats.FindAsync(id);
            if (data != null)
            {
                data.IsActive = false;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CheckDuplicateFlat(string name, long buildingId)
        {
            var checkDuplicateFlat = await context.Flats.FirstOrDefaultAsync(x => x.Name == name && x.BuildingId == buildingId && x.IsActive == true);
            bool returnType = checkDuplicateFlat != null;
            return returnType;
        }
    }

    public interface IFlatServices
    {
        Task<List<object>> GetFlats();
        Task<object> GetFlat(long id);
        Task<object> AddFlat(FlatDto flat);
        Task<object> UpdateFlat(FlatDto flat);
        Task<bool> DeleteFlat(long id);
        Task<bool> CheckDuplicateFlat(string name, long buildingId);
    }
}
