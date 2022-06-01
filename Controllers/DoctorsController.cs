using Data;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Controllers;

[Route("[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly AppointmentsDbContext _context;
    public DoctorsController(AppointmentsDbContext context)
    {
        // dependency injection
        _context = context;
    }

    // GET:Doctors
    [HttpGet]
    public IActionResult GetAll()
    {
        var doctors = _context.Doctors.ToList();
        return Ok(doctors);
    }

    // POST: Doctors
}