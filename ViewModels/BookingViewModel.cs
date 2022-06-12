namespace Doctor_Appointment_System.ViewModels
{
    public class BookingViewModel
    {
        public DateTime AppointmentTime { get; set; }
        public int TimeSlotId { get; set; }
        public string PaymentMethod { get; set; } = null!;
    }
}