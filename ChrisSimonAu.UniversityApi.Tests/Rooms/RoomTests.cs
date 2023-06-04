namespace ChrisSimonAu.UniversityApi.Tests.Rooms;

using Microsoft.AspNetCore.Mvc.Testing;

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
        var api = new RoomApi(_factory.CreateClient());

        var response = await api.SetupRoom();

        ItShouldSetupANewRoom(response);
    }

    private void ItShouldSetupANewRoom(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}