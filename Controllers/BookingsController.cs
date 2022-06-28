using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Data;
using Doctor_Appointment_System.Helpers;
using Doctor_Appointment_System.Models;
using Doctor_Appointment_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
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

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] BookingViewModel ViewModel)
    {
        var timeSlot = await _context.Timeslots
        .Include(ts => ts.Schedule).ThenInclude(s => s.Doctor)
        .Include(ts => ts.Bookings)
        .SingleOrDefaultAsync(ts => ts.Id == ViewModel.TimeSlotId);
        if (timeSlot == null)
        {
            return BadRequest("Selected Time-slot couldn't be recognized");
        }

        if (ViewModel.AppointmentTime < DateTime.Today)
        {
            return BadRequest("Selected Appointment-time can't be in the past time!😒");
        }

        if (ViewModel.AppointmentTime.DayOfWeek != timeSlot.Schedule.Day)
        {
            return BadRequest("Doctor is not available at the selected day! 😢🤷‍♂️");
        }

        if (timeSlot.MaxAppointments <= timeSlot.Bookings.Count)
        {
            return BadRequest("Wu kaa buuxa maanta, Sorry!😥");
        }

        var ticketPrice = timeSlot.Schedule.Doctor.TicketPrice;
        var Rate = 0.02m; // rate per person
        var Commission = ticketPrice * Rate;

        // TODO : Add real payment gateway (eDahab, Zaad)



        var transactionId = new Random().Next(10_000, 999_999);
        var booking = new Booking
        {
            AppointmentTime = new DateTime(ViewModel.AppointmentTime.Ticks, DateTimeKind.Utc),
            IsCompleted = false,
            UserId = User.GetId(), // Logged in User
            CreatedAt = DateTime.UtcNow,
            TransactionId = $"TR{transactionId}",
            PaidAmount = ticketPrice,
            Commission = Commission,
            DoctorRevenue = ticketPrice - Commission,
            PaymentMethod = ViewModel.PaymentMethod,
            TimeslotId = timeSlot.Id,
        };

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();

        return Created("", booking);
    }
}