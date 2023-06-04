namespace ChrisSimonAu.UniversityApi.Tests.Rooms;

public class RoomApi
{
    private readonly HttpClient client;

    public RoomApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<HttpResponseMessage> SetupRoom()
    {
        return await client.PostAsync("/rooms", null);
    }
}