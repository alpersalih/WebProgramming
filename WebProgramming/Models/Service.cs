using System.ComponentModel.DataAnnotations;

namespace WebProgramming.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hizmet adı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Süre zorunludur.")]
        public int Duration { get; set; } // Dakika

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        public decimal Price { get; set; } // Ücret
    }
}
