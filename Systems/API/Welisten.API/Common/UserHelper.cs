using System.Security.Claims;

namespace Welisten.API.Common;

public static class UserHelper
{
    public static bool IsExpired(ClaimsPrincipal user)
    {
        // Check if the "exp" claim exists and is a valid DateTime
        var expirationClaim = user.FindFirst("exp");

        if (expirationClaim != null && long.TryParse(expirationClaim.Value, out long unixTimestamp))
        {
            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
            if (expirationTime > DateTime.UtcNow)
                return false;
            else
                return true;
        }
        return true;
    }
}