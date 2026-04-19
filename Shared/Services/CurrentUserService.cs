using AmarBariAPI.Shared.Enum;

namespace AmarBariAPI.Shared.Services
{
    public class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor contextAccessor = contextAccessor;

        private int GetUserId()
        {
            var userId = CurrentContext?.User?.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (userId is not null)
                return Convert.ToInt32(userId);

            return -1;
        }

        private UserType? GetUserType()
        {
            var claimValue = CurrentContext?.User?.Claims?.FirstOrDefault(x => x.Type == "UserType")?.Value;

            if (string.IsNullOrWhiteSpace(claimValue))
            return null;

            if (System.Enum.TryParse<UserType>(claimValue, true, out var userType))
                return userType;

            return null;
        }

        public HttpContext CurrentContext
        {
            get => GetCurrentContext();
        }

        private HttpContext GetCurrentContext()
        {
            return contextAccessor.HttpContext!;
        }

        public int UserId
        {
            get => GetUserId();
        }

        public UserType? UserType
        {
            get => GetUserType();
        }

    }

    public interface ICurrentUserService
    {
        public int UserId { get; }
        public UserType? UserType { get; }
    }
}
