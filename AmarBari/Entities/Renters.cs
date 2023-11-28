using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBari.Entities
{
    public class Renters
    {
        [Key]
        public long Id { get; set; }
        public long FlatId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string RenterName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? FatherName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string MaritalStatus { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? PermanentAddress { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Occupation { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? Religion { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? AcademicQualification { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string MobileNo { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string NidNo { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? ImageUrl { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? DocumentImageUrl { get; set; }

        public DateTime RentDate { get; set; }
        public decimal AdvancedPaymet { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }


        [ForeignKey("FlatId")]
        public virtual Flats Flat { get; set; } = null!;
        public virtual List<MonthlyBills> MonthlyBills { get; set; }
    }
}
