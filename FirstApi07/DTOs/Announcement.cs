using System.ComponentModel.DataAnnotations;

namespace FirstApi07.DTOs
{
    public class Announcement
    {
        [Key]
        public Guid? IdAnnouncement { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        public DateTime? ValidFrom { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        public DateTime? ValidTo { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        [MaxLength(250, ErrorMessage = "Campul title poate contine maxim 250 caractere")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        [StringLength(250, ErrorMessage = "Campul Text poate contine maxim 250 caractere")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        public DateTime? EventDate { get; set; }

        [StringLength(1000, ErrorMessage = "Campul Tags poate contine maxim 1000 caractere")]
        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        [MinLength(5, ErrorMessage = "Campul poate avea minim 5 caractere")]
        public string Tags { get; set; }
    }
}
