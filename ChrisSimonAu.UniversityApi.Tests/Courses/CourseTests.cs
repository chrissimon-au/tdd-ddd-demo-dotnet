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
        var api = new CourseApi(_factory.CreateClient());

        var (response, course) = await api.IncludeInCatalog();
        
        ItShouldIncludeTheCourseInTheCatalog(response);
        ItShouldAllocateANewId(course);
        ItShouldShowWhereToLocateNewCourse(response, api.UriForCourseId(course?.Id));
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

    private void ItShouldShowWhereToLocateNewCourse(HttpResponseMessage response, Uri courseUri)
    {
        var location = response.Headers.Location;
        Assert.NotNull(location);
        Assert.Equal(courseUri, location);
    }
}