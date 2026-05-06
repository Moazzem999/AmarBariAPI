namespace AmarBariAPI.Dtos.Shop
{
    public class ContractRequestDto
    {
        public long Id { get; set; }
        public long? ShopRenterId { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public IFormFile? File { get; set; }
    }
}
