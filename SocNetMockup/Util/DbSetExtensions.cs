using System;
using System.Linq;
using System.Security.Claims;
using SocNetMockup.Models;

namespace SocNetMockup.Util
{
    public static class DbSetExtensions
    {
        public static User Get(this IQueryable<User> dbSet, ClaimsPrincipal principal)
        {
            var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null) throw new Exception("User's NameIdentifier claim is not found, user is most likely not authenticated.");

            return dbSet.First(x => x.Id == Guid.Parse(idClaim.Value));
        }

        public static bool TryGet(this IQueryable<User> dbSet, ClaimsPrincipal principal, out User user)
        {
            var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null) {
                user = null;
                return false;
            }

            user = dbSet.FirstOrDefault(x => x.Id == Guid.Parse(idClaim.Value));

            if (user == null) {
                return false;
            }

            return true;
        }
    }
}
