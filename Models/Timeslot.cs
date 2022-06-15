namespace Doctor_Appointment_System.Models;
    public class Timeslot
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Description { get; set; }= "";

        public int MaxAppointments { get; set; }
        public List<Booking> Bookings { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }