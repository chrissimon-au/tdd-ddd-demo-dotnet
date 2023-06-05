namespace ChrisSimonAu.UniversityApi.Tests.Rooms;

public class RoomApi
{
    private readonly HttpClient client;

    public RoomApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<(HttpResponseMessage, RoomResponse?)> SetupRoom()
    {
        var response = await client.PostAsync("/rooms", null);
        var room = await response.Content.ReadFromJsonAsync<RoomResponse>();
        return (response, room);
    }

    public Uri UriForRoomId(Guid? roomId)
    {
        return new Uri($"http://localhost/rooms/{roomId}");
    }
}