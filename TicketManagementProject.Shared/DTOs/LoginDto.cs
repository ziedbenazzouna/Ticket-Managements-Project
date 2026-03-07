using System.ComponentModel.DataAnnotations;

namespace TicketManagementProject.Shared.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "L'adresse email est obligatoire")]
        [EmailAddress(ErrorMessage = "Veuillez saisir un email valide")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public string Password { get; set; } = "";
    }
}
