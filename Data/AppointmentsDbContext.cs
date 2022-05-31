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
}