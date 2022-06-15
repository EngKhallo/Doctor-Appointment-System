using Data;
using ViewModels;
using Microsoft.AspNetCore.Mvc;
using Doctor_Appointment_System.Models;
using Doctor_Appointment_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class SchedulesController : ControllerBase
{
    private readonly AppointmentsDbContext _context;
    public SchedulesController(AppointmentsDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // only show the logged in Doctor's Schedules
        var doctorId = 2; // TODO: Get the actual doctorId from the session
        var schedules =await _context.Schedules.
                    Include(s => s.TimeSlots)
                    .Where(s => s.DoctorId == doctorId)
                    .ToListAsync();

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
            DoctorId = 2, // TODO
            IsAvailable = true,
        };

        _context.Schedules.Add(schedule);
        _context.SaveChanges();

        return Created("", schedule);
    }

    // PUT /schedules/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] ModifyScheduleViewModel viewModel)
    {
        var schedule = _context.Schedules.Find(id);
        if (schedule is null)
        {
            return BadRequest("Invalid Schedule");
        }
        // Todo : Only owner of the schedule can update the schedule

        schedule.Location = viewModel.Location;
        schedule.Day = viewModel.Day;
        schedule.IsAvailable = viewModel.IsAvailable;

       _context.SaveChanges();

       return Ok(); 
    }

    // POST /schedules/{id}/timeslots
    [HttpPost("{id}/timeslots")]
    public IActionResult AddTimeSlot(int id, [FromBody] TimeSlotViewModel viewModel)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var schedule = _context.Schedules.Find(id);
        if (schedule is null)
        {
            return BadRequest($"Schedule with id {id} can not be found");
        }

        var timeslot = new Timeslot
        {
            StartTime = viewModel.StartTime,
            EndTime = viewModel.EndTime,
            Description = viewModel.Description,
            MaxAppointments = viewModel.MaxAppointments,
            ScheduleId = schedule.Id,
            CreatedAt = DateTime.UtcNow,
            Schedule = schedule
        };

        _context.Timeslots.Add(timeslot);
        _context.SaveChanges();

        return Created("", timeslot);
    }


}