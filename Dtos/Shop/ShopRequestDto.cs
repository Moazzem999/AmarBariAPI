namespace AmarBariAPI.Dtos.Shop
{
    public class ShopRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MarketName { get; set; } = string.Empty;
        public string ShopNumber { get; set; } = string.Empty;
        public decimal CurrentRent { get; set; }
    }
}
