using AmarBariAPI.Entities.Common;
using AmarBariAPI.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBariAPI.Entities
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTimeOffset? DateOfBirth { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public UserType UserType { get; set; } = UserType.Owner;
    }
}
