using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HotelBooking.xUnit.IntegrationTest
{
    public class BasicTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public BasicTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home")]
        [InlineData("/Home/About")]
        [InlineData("/Home/Rooms")]
        [InlineData("/Account/Register")]
        [InlineData("/Account/Login")]
        [InlineData("/Room")]
        [InlineData("/Room/Index")]
        [InlineData("/Room/NewRooms")]
        [InlineData("/Reservation/CreateReservation")]
        [InlineData("/Reservation/Reservation/21")]
        [InlineData("/Reservation/DetailReservation/42")]
        [InlineData("/Reservation/EditReservation/42")]
        [InlineData("/Reservation/ReservationPDF/42")]
        public async Task GetHttpRequest(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    
        [Fact]
        public async Task Index_WhenCalled_ReturnsApplicationForm()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/WebApi/GetUsers");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Michael", responseString);
            Assert.Contains("Jordan", responseString);
        }
    }
}
