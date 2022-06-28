using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Helpers;
public static class UserExtension
{
    public static int GetId(this ClaimsPrincipal principal) // this keyword : extension, inherits every class
    {
         var isConversionSuccessful = int.TryParse(principal.FindFirstValue(JwtRegisteredClaimNames.Sub), out var userId); // whole user claim : id with its info
        if (isConversionSuccessful)
        {
            return userId;
        }

        return 0;
    }
}