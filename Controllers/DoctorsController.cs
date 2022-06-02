using Data;
using Doctor_Appointment_System.Models;
using Doctor_Appointment_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var doctors = _context.Doctors.Include(d => d.User).ToList();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public IActionResult GetSingle(int id)
    {
        var doctor = _context.Doctors.Include(d => d.User).SingleOrDefault(d => d.Id == id);
        if (doctor is null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpPost]
    public IActionResult Add([FromBody] DoctorViewModel doctorViewModel) // Over-posting attack.
    {
        var doctor = new Doctor
        {
            Phone = doctorViewModel.Phone,
            Specialty = doctorViewModel.Specialty,
            Bio = doctorViewModel.Bio,
            Certificate = doctorViewModel.Certificate,
            CreatedAt = DateTime.UtcNow,
            Picture = doctorViewModel.Picture,
            TicketPrice = doctorViewModel.TicketPrice,
            UserId = 2 // TODO : Use the currently logged in UserId
        };
        _context.Doctors.Add(doctor);
        _context.SaveChanges();

        return Created("", doctor);
    }


}