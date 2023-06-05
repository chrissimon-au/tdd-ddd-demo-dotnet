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

        var (response, room) = await api.SetupRoom();

        ItShouldSetupANewRoom(response);
        ItShouldAllocateANewId(room);
        ItShouldShowWhereToLocateNewRoom(response, api.UriForRoomId(room?.Id));
    }

    private void ItShouldSetupANewRoom(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    private void ItShouldAllocateANewId(RoomResponse? room)
    {
        Assert.NotNull(room);
        Assert.NotEqual(room.Id, Guid.Empty);
    }

    private void ItShouldShowWhereToLocateNewRoom(HttpResponseMessage response, Uri roomUri)
    {
        var location = response.Headers.Location;
        Assert.NotNull(location);
        Assert.Equal(roomUri, location);
    }
}