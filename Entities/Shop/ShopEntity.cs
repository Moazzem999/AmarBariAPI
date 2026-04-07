using AmarBariAPI.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities.Shop
{
    [Table("Shops")]
    public class ShopEntity : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string MarketName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ShopNumber { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentRent { get; set; }

        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; } = null!;

        public virtual ICollection<ShopRenterEntity> ShopRenters { get; set; } = [];
    }
}
