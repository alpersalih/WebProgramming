using System.ComponentModel.DataAnnotations;

namespace WebProgramming.Models
{
    public class Salon
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Salon adı zorunludur.")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [RegularExpression(@"^(\d{3})-(\d{3})-(\d{4})$", ErrorMessage = "Telefon numarası 555-555-5555 formatında olmalıdır.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Çalışma saatleri zorunludur.")]
        [RegularExpression(@"^([0-1][0-9]|2[0-1]):[0-5][0-9] - ([0-1][0-9]|2[0-1]):[0-5][0-9]$",
            ErrorMessage = "Çalışma saatleri '09:00 - 21:00' formatında olmalıdır.")]
        public string WorkingHours { get; set; }
    }
}
