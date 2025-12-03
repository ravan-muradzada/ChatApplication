using ChatApplication.Domain.CustomExceptions;
using System.Security.Claims;

namespace ChatApplication.WebAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            string? strUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(strUserId, out var parsed))
            {
                throw new InvalidAccessTokenException("User Id is missing or invalid!");
            }

            return parsed;
        }
    }
}
