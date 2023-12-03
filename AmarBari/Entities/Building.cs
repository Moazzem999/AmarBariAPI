using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AmarBari.Enum;

namespace AmarBari.Entities
{
    public class Building
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Description { get; set; }
        
        public ElectricMeterType ElectricMeterType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }


        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
        public virtual List<Flat> Flats { get; set; }
    }
}
