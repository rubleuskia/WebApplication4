using System.Linq;
using System.Security.Claims;

namespace WebApplication4.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal) =>
            principal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
    }
}