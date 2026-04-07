using AmarBariAPI.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities.Shop
{
    public class ContractEntity : BaseEntity
    {
        public long ShopRenterId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        [ForeignKey("ShopRenterId")]
        public virtual ShopRenterEntity ShopRenter { get; set; } = null!;
    }
}
