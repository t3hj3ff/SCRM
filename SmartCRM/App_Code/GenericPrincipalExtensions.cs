using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;


public static class GenericPrincipalExtensions
{
    public static string GetFullName(this IPrincipal user)
    {
        if (user.Identity.IsAuthenticated)
        {
            ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == "FullName")
                    return claim.Value;
            }
            return "";
        }
        else
            return "";
    }


    public static string GetClaim(this IPrincipal user, string claim_name)
    {
        if (user.Identity.IsAuthenticated)
        {
            ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == claim_name)
                    return claim.Value;
            }
            return "";
        }
        else
            return "";
    }

    public static void AddClaim(this IPrincipal user, string claim_name, string claim_value)
    {
        if (user.Identity.IsAuthenticated)
        {
            ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
          
            var _claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == claim_name);
            if (_claim != null)
            {
                claimsIdentity.RemoveClaim(_claim);
            }

            claimsIdentity.AddClaim(new Claim(claim_name, claim_value));
        }
    }
}
