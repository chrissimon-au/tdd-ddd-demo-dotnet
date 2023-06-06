namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

using Microsoft.AspNetCore.Mvc.Testing;

public class SchedulingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SchedulingTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenIAmAnAdmin_WhenIScheduleCourses()
    {
        var schedulingApi = new SchedulingApi(_factory.CreateClient());

        var response = await schedulingApi.Schedule();

        ItShouldScheduleTheCourses(response);
    }

    private void ItShouldScheduleTheCourses(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

}