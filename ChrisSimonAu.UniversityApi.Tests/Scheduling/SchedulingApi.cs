namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

public class SchedulingApi
{
    private readonly HttpClient client;

    public SchedulingApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<(HttpResponseMessage, ScheduleResponse?)> Schedule()
    {
        var response = await client.PostAsync("/schedules", null);
        var schedule = await response.Content.ReadFromJsonAsync<ScheduleResponse>();
        return (response, schedule);
    }
}