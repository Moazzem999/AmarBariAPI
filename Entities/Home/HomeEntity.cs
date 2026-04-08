using AmarBariAPI.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities.Home
{
    [Table("Homes")]
    public class HomeEntity : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; } = null!;

        public virtual ICollection<FlatEntity> Flats { get; set; } = [];
    }
}
