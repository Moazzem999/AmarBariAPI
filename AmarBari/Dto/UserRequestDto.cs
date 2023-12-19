using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBari.Dto
{
    public class UserRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string NidNo { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? NidImage { get; set; }
        public string? Password { get; set; }
    }
}
