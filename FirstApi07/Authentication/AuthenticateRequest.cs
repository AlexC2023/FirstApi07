using System.ComponentModel.DataAnnotations;

namespace FirstApi07.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public Guid username { get; set; } //idMember - tabela Member

        [Required]
        public string Password { get; set; } //name -tabela Memeber
    }
}
