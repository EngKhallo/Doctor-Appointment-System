using Data;
using ViewModels;
using Microsoft.AspNetCore.Mvc;
using Doctor_Appointment_System.Models;

namespace Controllers;
[Route("[controller]")]

public class SchedulesController : ControllerBase
{
    private readonly AppointmentsDbContext _context;
    public SchedulesController(AppointmentsDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        // only show the logged in Doctor's Schedules
        var doctorId = 2; // TODO: Get the actual doctorId from the session
        var schedules = _context.Schedules.Where(s => s.DoctorId == doctorId).ToList();

        return Ok(schedules);
    }

    [HttpPost]
    public IActionResult Add(SchedulesViewModel viewModel)
    {
        var schedule = new Schedule
        {   
            Day = viewModel.Day,
            Location = viewModel.Location,
            CreatedAt = DateTime.UtcNow,
            DoctorId = 1, // TODO
            IsAvailable = true,
        };

        _context.Schedules.Add(schedule);
        _context.SaveChanges();

        return Created("", schedule);
    }
}