namespace Doctor_Appointment_System.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; } // pre-made enum 
        public string Location { get; set; } = "";
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Timeslot> TimeSlots { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}