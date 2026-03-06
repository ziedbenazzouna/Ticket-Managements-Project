using System.ComponentModel.DataAnnotations;

namespace TicketManagementProject.Blazor.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "L'adresse email est obligatoire")]
        [EmailAddress(ErrorMessage = "Veuillez saisir un email valide")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public string Password { get; set; } = "";
    }
}
