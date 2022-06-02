namespace Doctor_Appointment_System.ViewModels;
public class DoctorViewModel
{
    // In here we put the information needed for the user to send it only  
    public string Phone { get; set; } = "";
    public string Specialty { get; set; } = "";
    public string Picture { get; set; } = "";
    public string Bio { get; set; } = "";
    public string Certificate { get; set; } = "";
    public decimal TicketPrice { get; set; }
}