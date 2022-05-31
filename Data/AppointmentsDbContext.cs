using Doctor_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppointmentsDbContext : DbContext
{
    public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Timeslot> Timeslots { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingNote> Notes { get; set; }
    public DbSet<Review> Reviews { get; set; }
}