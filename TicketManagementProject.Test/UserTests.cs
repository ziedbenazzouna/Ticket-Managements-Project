using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace TicketManagementProject.Test
{
    public class UserTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ShouldReturn_JwtToken()
        {
            // Arrange
            var loginDto = new
            {
                Username = "mohamed@email.com",
                Password = "123456"
            };

            var json = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/login", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(responseString);

            Assert.NotNull(data.token);
            Assert.False(string.IsNullOrEmpty((string)data.token));
        }
    }
}