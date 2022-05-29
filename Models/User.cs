namespace Doctor_Appointment_System.Models;
    public class User
    {
        public int Id { get; set; }
        // '?' means this property can be null
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Address { get; set; }
        public string Gender { get; set; } = "";
        public bool IsDisabled { get; set; } // has a default value of false
        public DateTime CreatedAt { get; set; }
    }
    