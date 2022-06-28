using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Doctor_Appointment_System.Controllers;
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppointmentsDbContext _context;

    public AuthController(AppointmentsDbContext context)
    {
        _context = context;
    }
    // POST /auth/login : Username and password
    [HttpPost("login")]
    public async Task<IActionResult> Login(string email)
    {

        // validate user information : user availability

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user is null)
        {
            return BadRequest("Invalid Login Attempt");
        }

        var now = DateTime.Now;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // constant : which means sub
            new("FullName", user.FullName),
            new("gender", user.Gender),
            new("Email", user.Email),
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