namespace ChrisSimonAu.UniversityApi.Tests.Courses;

using Microsoft.AspNetCore.Mvc.Testing;

public class CourseTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CourseTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenIAmAnAdmin_WhenIIncludeANewCourseInTheCatalog()
    {
        HttpClient client = _factory.CreateClient();

        var response = await client.PostAsync("/courses", null);
        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();

        ItShouldIncludeTheCourseInTheCatalog(response);
        ItShouldAllocateANewId(course);
    }

    private void ItShouldIncludeTheCourseInTheCatalog(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    private void ItShouldAllocateANewId(CourseResponse? course)
    {
        Assert.NotNull(course);
        Assert.NotEqual(Guid.Empty, course.Id);
    }
}