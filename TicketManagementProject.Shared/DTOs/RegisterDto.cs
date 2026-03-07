using System.ComponentModel.DataAnnotations;

namespace TicketManagementProject.Shared.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*[^a-zA-Z0-9]).+$",
    ErrorMessage = "Le mot de passe doit contenir au moins une lettre et un caractère spécial (ex: @, #, !, .).")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Veuillez confirmer le mot de passe")]
        [Compare(nameof(Password), ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; } = "";
    }
}
