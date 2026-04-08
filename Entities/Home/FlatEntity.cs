using AmarBariAPI.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities.Home
{
    [Table("Flats")]
    public class FlatEntity : BaseEntity
    {
        public long HomeId { get; set; }

        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Floor { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentRent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GasBill { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal WaterBill { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ServiceCharge { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal OthersBill { get; set; }

        [ForeignKey("HomeId")]
        public virtual HomeEntity Home { get; set; } = null!;

        public virtual ICollection<FlatRenterEntity> FlatRenters { get; set; } = [];
    }
}
