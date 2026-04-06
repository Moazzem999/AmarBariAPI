using AmarBariAPI.Entities.Common;
using AmarBariAPI.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities.Shop
{
    public class ShopRenterEntity : BaseEntity
    {
        public long ShopId { get; set; }

        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string FatherName { get; set; } = string.Empty;

        public DateTimeOffset? DateOfBirth { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Religion Religion { get; set; }

        [MaxLength(500)]
        public string PresentAddress { get; set; } = string.Empty;

        [MaxLength(500)]
        public string PermanentAddress { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Occupation { get; set; } = string.Empty;

        [MaxLength(50)]
        public string AcademicQualification { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Mobile { get; set; } = string.Empty;

        [MaxLength(50)]
        public string NidNo { get; set; } = string.Empty;

        public DateTimeOffset? RentDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AdvancedPaymet { get; set; }

        [MaxLength(255)]
        public string? ImagePath { get; set; }

        [ForeignKey("ShopId")]
        public virtual ShopEntity Shop { get; set; } = null!;
    }
}
