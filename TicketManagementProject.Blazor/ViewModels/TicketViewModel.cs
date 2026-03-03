using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TicketManagementProject.Blazor.Enum;

namespace TicketManagementProject.Blazor.ViewModels
{
    //public class TicketViewModel
    //{
    //    public string? Id { get; set; }
    //   [Required] public string Objet { get; set; }
    //   [Required] public string Auteur { get; set; }
    //   [Required] public DateTime Date { get; set; }
    //   [Required] public string Categorie { get; set; }
    //   [Required] public string Statut { get; set; }
    //}

    public class TicketViewModel
    {
        public string? Id { get; set; }
        [Required] public string Objet { get; set; }
        [Required] public string Auteur { get; set; }
        [Required] public DateTime Date { get; set; } = DateTime.Now;

        // Champs stockés en base (Strings pour l'API)
        [Required] public string Categorie { get; set; } = CategorieIntervention.Fuite_Eau.ToString();
        [Required] public string Statut { get; set; } = StatutIntervention.New.ToString();

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
