using AmarBariAPI.Dtos.Common;
using AmarBariAPI.Shared.Enum;

namespace AmarBariAPI.Dtos.Home
{
    public class FlatRenterResponseDto : BaseDto
    {
        public long FlatId { get; set; }
        public string FlatName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string NidNo { get; set; } = string.Empty;
        public DateTimeOffset? DateOfBirth { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Religion Religion { get; set; }
        public string Occupation { get; set; } = string.Empty;
        public string AcademicQualification { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string PresentAddress { get; set; } = string.Empty;
        public string PermanentAddress { get; set; } = string.Empty;
        public DateTimeOffset? RentDate { get; set; }
        public decimal AdvancedPaymet { get; set; }
        public string? ImagePath { get; set; }
    }
}
