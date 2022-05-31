namespace Doctor_Appointment_System.Models;
public class BookingNote
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = new();
    public string Note { get; set; }= "";
    public DateTime CreatedAt { get; set; }

}