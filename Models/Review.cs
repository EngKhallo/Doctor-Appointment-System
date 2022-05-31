namespace Doctor_Appointment_System.Models;
    public class Review
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = new();
        public int Stars { get; set; }
        public string Remarks { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }