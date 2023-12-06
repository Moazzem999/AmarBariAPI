using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBari.Dto
{
    public class FlatDto
    {
        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Floor { get; set; }
    }
}
