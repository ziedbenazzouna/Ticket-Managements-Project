using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketManagementProject.API.Entities
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // Mongo va générer automatiquement si null
        public string Objet { get; set; }
        public string Auteur { get; set; }
        public DateTime Date { get; set; }
        public string Categorie { get; set; }
        public string Statut { get; set; }

        [BsonElement("Commentaires")]
        [BsonIgnoreIfNull]
        public List<TicketComment> Commentaires { get; set; } = new();
    }
}
