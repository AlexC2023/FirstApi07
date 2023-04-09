using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace FirstApi07.DTOs.PatchObject
{
    public class PatchAnnouncement
    {
        [Key]
        [JsonIgnore]
        public Guid? IdAnnouncement { get; set; }

        
        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        
        [MaxLength(250, ErrorMessage = "Campul title poate contine maxim 250 caractere")]
        public string? Title { get; set; }


       
        [StringLength(250, ErrorMessage = "Campul Text poate contine maxim 250 caractere")]
        public string? Text { get; set; }

      
        public DateTime? EventDate { get; set; }

        [StringLength(1000, ErrorMessage = "Campul Tags poate contine maxim 1000 caractere")]
       
       
        public string? Tags { get; set; }
    }
}
