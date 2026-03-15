using AmarBariAPI.Shared.Enum;

namespace AmarBariAPI.Dtos.Common
{
    public class BaseDto
    {
        public long Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public Status Status { get; set; }
    }
}
