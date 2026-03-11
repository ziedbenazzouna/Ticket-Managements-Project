using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using TicketManagementProject.API.Entities;
using Xunit;

namespace TicketManagementProject.Test
{
    public class TicketTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TicketTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> GetJwtToken()
        {
            var loginDto = new
            {
                Username = "mohamed@email.com",
                Password = "123456"
            };

            var json = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/login", content);
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine(error);

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            return data.token;
        }

        [Fact]
        public async Task GetAllTickets_ShouldReturn_List()
        {
            // Arrange
            var token = await GetJwtToken();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/tickets");

            // Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var tickets = JsonConvert.DeserializeObject<List<object>>(responseString);

            Assert.NotNull(tickets);
        }

        [Fact]
        public async Task CreateTicket_ShouldReturnOkAndTicket()
        {
            // Arrange
            var ticket = new Ticket
            {
                Objet = "Panne Ascenseur",
                Auteur = "Hedi",
                Date = DateTime.UtcNow,
                Categorie = "Hardware",
                Statut = "En cours"
            };

            var json = JsonConvert.SerializeObject(ticket);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var token = await GetJwtToken();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync("/api/tickets", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var createdTicket = JsonConvert.DeserializeObject<Ticket>(responseString);

            Assert.NotNull(createdTicket);
            Assert.Equal(ticket.Objet, createdTicket.Objet);
            Assert.Equal(ticket.Auteur, createdTicket.Auteur);
            Assert.Equal(ticket.Categorie, createdTicket.Categorie);
            Assert.Equal(ticket.Statut, createdTicket.Statut);
        }
    }
}