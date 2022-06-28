using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Data;
using Doctor_Appointment_System.Helpers;
using Doctor_Appointment_System.Models;
using Doctor_Appointment_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
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
    public async Task<IActionResult> GetAll()
    {
        // Todo : Add pagination and filtering
        var doctors =await _context.Doctors.Include(d => d.User).ToListAsync();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        var doctor =await _context.Doctors.Include(d => d.User).SingleOrDefaultAsync(d => d.Id == id);
        if (doctor is null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] DoctorViewModel doctorViewModel) // Over-posting attack.
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
            UserId = User.GetId()
        };
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();

        return Created("", doctor);
    }


}