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

        var roomRequest = new SetupRoomRequest { Name = Guid.NewGuid().ToString(), Capacity = 5 };
        var (response, room) = await api.SetupRoom(roomRequest);

        ItShouldSetupANewRoom(response);
        ItShouldAllocateANewId(room);
        ItShouldShowWhereToLocateNewRoom(response, api.UriForRoomId(room?.Id));
        ItShouldConfirmRoomDetails(roomRequest, room);
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

    private void ItShouldConfirmRoomDetails(SetupRoomRequest roomRequest, RoomResponse? room)
    {
        Assert.Equal(roomRequest.Name, room?.Name);
        Assert.Equal(roomRequest.Capacity, room?.Capacity);
    }

    [Fact]
    public async Task GivenIHaveSetupARoom_WhenICheckItsDetails()
    {
        var api = new RoomApi(_factory.CreateClient());

        var roomRequest = new SetupRoomRequest { Name = "Test Room" };
        var (response, _) = await api.SetupRoom(roomRequest);

        var newRoomLocation = response.Headers.Location;

        var checkedResponse = await api.GetRoom(newRoomLocation);

        ItShouldFindTheNewRoom(checkedResponse);
    }

    private void ItShouldFindTheNewRoom(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}