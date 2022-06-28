using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Doctor_Appointment_System.Controllers;
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    // POST /auth/login : Username and password
    [HttpPost("login")]
    public IActionResult Login()
    {

        // TODO : validate user information : user availability
        var now = DateTime.Now;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, "1"), // constant : which means sub
            new("FullName", "User Full Name")
        };


        var KeyInput = "random_text_with_atleast_32_chars";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyInput));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken("MyAPI", "MyFrontendApp", claims, now, now.AddDays(1), credentials);

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.WriteToken(token);
        var result = new
        {
            token = jwt
        };
        return Ok(result);
    }
}