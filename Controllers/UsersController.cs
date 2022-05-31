using Data;
using Doctor_Appointment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Controllers;
// controller route/path
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppointmentsDbContext _context;
    public UsersController(AppointmentsDbContext context)
    {
        _context = context;
    }
    // GET /users
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    // Get /Users/5
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // Post:Users
    [HttpPost]
    public IActionResult Add([FromBody] User user)
    {
        _context.Users.Add(user); // saves data to the pc memory
        _context.SaveChanges(); // saves changes to the Database

        return Created("", user);
    }
}