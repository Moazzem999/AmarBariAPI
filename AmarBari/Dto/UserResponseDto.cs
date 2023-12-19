using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBari.Dto
{
    public class UserResponseDto
    {
        public long Id { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string NidNo { get; set; }
        public string ImageUrl { get; set; }
        public string NidImageUrl { get; set; }
        public string LoginId { get; set; }
        public bool? IsApproved { get; set; }
    }
}
