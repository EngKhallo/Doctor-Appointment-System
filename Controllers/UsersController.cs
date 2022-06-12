using Data;
using Doctor_Appointment_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Controllers;
// controller route/path
[Route("api/v1/[controller]")]
[ApiController]

public class UsersController : ControllerBase
{
    private readonly AppointmentsDbContext _context;
    public UsersController(AppointmentsDbContext context)
    {
        _context = context;
    }
    // GET /users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users =await _context.Users.OrderBy(u => u.Id).ToListAsync();
        return Ok(users);
    }

    // Get /Users/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user =await _context.Users.FindAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // Post:Users
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] User user)
    {
        await _context.Users.AddAsync(user); // saves data to the pc memory
        await _context.SaveChangesAsync(); // saves changes to the Database

        return Created("", user);
    }

    // PUT /users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
    {
        var targetUser =await _context.Users.FindAsync(id);
        if (targetUser is null)
        {
            return BadRequest();
        }

        targetUser.FullName = user.FullName;
        targetUser.Email = user.Email;
        targetUser.Address = user.Address;
        targetUser.Gender = user.Gender;

        _context.Users.Update(targetUser);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if(user is null){
            return BadRequest();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}