using Microsoft.AspNetCore.Mvc.Testing;

namespace ChrisSimonAu.UniversityApi.Tests;

public class RoomTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RoomTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }


    [Fact]
    public async Task GivenIAmAnAdmin_WhenISetupANewRoom()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsync("/rooms", null);

        ItShouldSetupANewRoom(response);
    }

    private void ItShouldSetupANewRoom(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}