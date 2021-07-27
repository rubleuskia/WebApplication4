using System.Linq;
using System.Security.Claims;

namespace Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}