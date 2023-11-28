using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AmarBari.Entities
{
    public class Users
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string UserType { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string NidNo { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string ImageUrl { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string NidImageUrl { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LoginId { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
        
        public bool? IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public long? RenterId { get; set; }

        public virtual List<Buildings> Buildings { get; set; }
    }
}
