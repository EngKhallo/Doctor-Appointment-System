using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Controllers;
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly AppointmentsDbContext _context;
    public BookingsController(AppointmentsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() // Task = void
    {
        var bookings = await _context.Bookings.ToListAsync();

        // CPU => Cores => Threads
        // CPU => have cores, and cores have threads : every core have 2 threads. e.g. CORE i5 = 10 threads

        // multithreading : asynchronous programming

        return Ok(bookings);
    }
}