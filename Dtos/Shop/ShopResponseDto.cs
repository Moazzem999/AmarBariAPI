using AmarBariAPI.Dtos.Common;

namespace AmarBariAPI.Dtos.Shop
{
    public class ShopResponseDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string MarketName { get; set; } = string.Empty;
        public string ShopNumber { get; set; } = string.Empty;
        public decimal CurrentRent { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
    }
}
