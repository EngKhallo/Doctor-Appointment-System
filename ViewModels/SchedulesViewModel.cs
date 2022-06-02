using System.ComponentModel.DataAnnotations;

namespace ViewModels;
    public class SchedulesViewModel
    {
        public DayOfWeek Day { get; set; }

        [Required]
        public string Location { get; set; } = "";


    }