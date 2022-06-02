namespace Doctor_Appointment_System.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } // navigation property
        public string Phone { get; set; } = "";
        public string Specialty { get; set; } = "";
        public string Picture { get; set; } = "";
        public string Bio { get; set; } = "";
        public string Certificate { get; set; } = "";
        public decimal TicketPrice { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}