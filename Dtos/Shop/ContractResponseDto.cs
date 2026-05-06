using AmarBariAPI.Dtos.Common;

namespace AmarBariAPI.Dtos.Shop
{
    public class ContractResponseDto : BaseDto
    {
        public long ShopRenterId { get; set; }
        public string ShopRenterName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string? FilePath { get; set; }
    }
}
