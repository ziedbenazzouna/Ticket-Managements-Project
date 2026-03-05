namespace TicketManagementProject.API.Entities
{
    public class TicketComment
    {
        public string Auteur { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
