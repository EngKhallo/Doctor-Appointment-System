using Data;
using Doctor_Appointment_System.Models;
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
        var doctors = _context.Doctors.OrderBy(d => d.Id).ToList();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public IActionResult GetSingle(int id)
    {
        var doctor = _context.Doctors.Find(id);
        if (doctor is null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpPost]
    public IActionResult Add([FromBody] Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();

        return Created("", doctor);
    }

}