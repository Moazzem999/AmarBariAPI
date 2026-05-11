using AmarBariAPI.Dtos.Common;

namespace AmarBariAPI.Dtos.Home
{
    public class HomeResponseDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
