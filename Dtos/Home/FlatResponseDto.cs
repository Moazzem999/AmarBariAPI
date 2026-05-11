using AmarBariAPI.Dtos.Common;

namespace AmarBariAPI.Dtos.Home
{
    public class FlatResponseDto : BaseDto
    {
        public long HomeId { get; set; }
        public string HomeName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        public decimal CurrentRent { get; set; }
        public decimal GasBill { get; set; }
        public decimal WaterBill { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal OthersBill { get; set; }
    }
}
