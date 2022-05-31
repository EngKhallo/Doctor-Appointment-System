using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Controllers;
// controller route/path
[Route("[controller]")]
public class UsersController : ControllerBase
{
    // GET /users
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("List of all Users");
    }
}