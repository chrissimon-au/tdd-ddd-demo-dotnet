namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

public class SchedulingApi
{
    private readonly HttpClient client;

    public SchedulingApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<HttpResponseMessage> Schedule()
    {
        return await client.PostAsync("/schedules", null);
    }
}