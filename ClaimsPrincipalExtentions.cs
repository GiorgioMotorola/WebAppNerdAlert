using System.Security.Claims;

namespace WebAppNerdAlert
{
    public static class ClaimsPrincipalExtentions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
