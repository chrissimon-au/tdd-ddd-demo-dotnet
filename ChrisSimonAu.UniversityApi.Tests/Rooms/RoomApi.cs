namespace ChrisSimonAu.UniversityApi.Tests.Rooms;

public class RoomApi
{
    private readonly HttpClient client;

    public RoomApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<(HttpResponseMessage, RoomResponse?)> SetupRoom(SetupRoomRequest roomRequest)
    {
        var response = await client.PostAsync("/rooms", JsonContent.Create(roomRequest));
        var room = await response.Content.ReadFromJsonAsync<RoomResponse>();
        return (response, room);
    }

    public Uri UriForRoomId(Guid? roomId)
    {
        return new Uri($"http://localhost/rooms/{roomId}");
    }

    public async Task<(HttpResponseMessage, RoomResponse?)> GetRoom(Uri? roomLocation)
    {
        var response = await client.GetAsync(roomLocation);
        var room = await response.Content.ReadFromJsonAsync<RoomResponse>();
        return (response, room);
    }

    public async Task<(HttpResponseMessage, RoomResponse?)> GetRoom(Guid id)
        => await GetRoom(UriForRoomId(id));
}