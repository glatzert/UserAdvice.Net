using System.Linq;
using System.Security.Claims;

namespace UserAdvice.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAuthenticated(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return false;

            return principal.Identities.Any(i => i.IsAuthenticated);
        }

        public static bool IsModerator(this ClaimsPrincipal principal, int? categoryId)
        {
            if (!principal.IsAuthenticated())
                return false;

            //TODO: Handle
            return false;
        }
    }
}
