using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
