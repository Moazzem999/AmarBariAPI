using System.ComponentModel;

namespace AmarBariAPI.Shared.Enum
{
    public enum UserType
    {
        [Description("Owner")]
        Owner = 1,
        [Description("Renter")]
        Renter = 2
    }
}
