using System.ComponentModel.DataAnnotations;

namespace TicketManagementProject.API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "L'adresse email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Veuillez saisir une adresse email valide.")]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
