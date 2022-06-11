using Data;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Controllers;
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly AppointmentsDbContext _context;
    public BookingsController(AppointmentsDbContext context)
    {
        _context = context;
    }
}