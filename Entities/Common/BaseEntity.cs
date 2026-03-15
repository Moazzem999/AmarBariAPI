using AmarBariAPI.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedOn { get; set; } = DateTimeOffset.UtcNow;
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
}
