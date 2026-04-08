using AmarBariAPI.Entities.Common;
using AmarBariAPI.Entities.Home;
using AmarBariAPI.Entities.Shop;
using AmarBariAPI.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        public DateTimeOffset? DateOfBirth { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UserName { get; set; }

        [MaxLength(255)]
        public string? Password { get; set; }
        public UserType UserType { get; set; } = UserType.Owner;

        [MaxLength(255)]
        public string? ImagePath { get; set; }

        public virtual ICollection<ShopEntity> Shops { get; set; } = [];
        public virtual ICollection<HomeEntity> Homes { get; set; } = [];
    }
}
