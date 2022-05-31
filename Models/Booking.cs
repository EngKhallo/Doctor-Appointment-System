namespace Doctor_Appointment_System.Models;
public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime AppointmentTime { get; set; }
    public int TimeslotId { get; set; }
    public Timeslot? Timeslots { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Commission { get; set; }
    public decimal DoctorRevenue { get; set; }
    public string TransactionId { get; set; } = "";
    public string paymentMethod { get; set; } = "";
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

}