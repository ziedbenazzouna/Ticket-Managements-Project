using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TicketManagementProject.Blazor.Enum;

namespace TicketManagementProject.Blazor.ViewModels
{
    public class TicketViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Veuillez saisir l'objet du ticket.")]
        public string Objet { get; set; }

        [Required(ErrorMessage = "Le nom de l'auteur est obligatoire.")]
        public string Auteur { get; set; }

        [Required(ErrorMessage = "La date est obligatoire.")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Veuillez sélectionner une catégorie.")]
        public string Categorie { get; set; } = CategorieIntervention.Fuite_Eau.ToString();

        [Required(ErrorMessage = "Le statut est obligatoire.")]
        public string Statut { get; set; } = StatutIntervention.New.ToString();

        public List<CommentViewModel> Commentaires { get; set; } = new();

        // Propriétés d'aide pour l'interface UI (Non sérialisées)
        [JsonIgnore]
        public CategorieIntervention CategorieEnum
        {
            get => System.Enum.TryParse<CategorieIntervention>(Categorie, out var result) ? result : CategorieIntervention.Fuite_Eau;
            set => Categorie = value.ToString();
        }

        [JsonIgnore]
        public StatutIntervention StatutEnum
        {
            get => System.Enum.TryParse<StatutIntervention>(Statut, out var result) ? result : StatutIntervention.New;
            set => Statut = value.ToString();
        }
    }

}
