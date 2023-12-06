using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBari.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string NidNo { get; set; }
        public string ImageUrl { get; set; }
        public string NidImageUrl { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public long? RenterId { get; set; }
    }
}
