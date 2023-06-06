namespace ChrisSimonAu.UniversityApi.Tests.Courses;

using ChrisSimonAu.UniversityApi.Tests.Rooms;
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
        var roomApi = new RoomApi(_factory.CreateClient());

        var (_, room) = await roomApi.SetupRoom(new SetupRoomRequest { Name = "Test Room"} );

        var request = new IncludeCourseInCatalogRequest { Name = Guid.NewGuid().ToString(), RoomId = room?.Id };

        var (response, course) = await api.IncludeInCatalog(request);
        
        ItShouldIncludeTheCourseInTheCatalog(response);
        ItShouldAllocateANewId(course);
        ItShouldShowWhereToLocateNewCourse(response, api.UriForCourseId(course?.Id));
        ItShouldConfirmCourseDetails(request, course);
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

    private void ItShouldConfirmCourseDetails(IncludeCourseInCatalogRequest request, CourseResponse? response)
    {
        Assert.Equal(request.Name, response?.Name);
        Assert.Equal(request.RoomId, response?.RoomId);
    }

    [Theory]
    [InlineData("Test Course")]
    [InlineData("Another Course")]
    public async Task GivenIHaveIncludedACourse_WhenICheckTheCourseDetails(string courseName)
    {
        var api = new CourseApi(_factory.CreateClient());
        var roomApi = new RoomApi(_factory.CreateClient());

        var (_, room) = await roomApi.SetupRoom(new SetupRoomRequest { Name = "Test Room"} );

        var request = new IncludeCourseInCatalogRequest { Name = courseName, RoomId = room?.Id };

        var (response, _) = await api.IncludeInCatalog(request);

        var newCourseLocation = response.Headers.Location;

        var (checkedResponse, checkedCourse) = await api.GetCourse(newCourseLocation);

        ItShouldFindTheNewCourse(checkedResponse);
        ItShouldConfirmCourseDetails(request, checkedCourse);
    }

    private void ItShouldFindTheNewCourse(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GivenIHaveNotSetupARoom_WhenIIncludeACourse()
    {
        var api = new CourseApi(_factory.CreateClient());
        
        var request = new IncludeCourseInCatalogRequest { Name = Guid.NewGuid().ToString() };

        var (response, course) = await api.IncludeInCatalog(request);

        ItShouldNotIncludeTheCourse(response);
    }

    private void ItShouldNotIncludeTheCourse(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GivenIHaveTheWrongRoomId_WhenIIncludeACourse()
    {
        var api = new CourseApi(_factory.CreateClient());
        
        var request = new IncludeCourseInCatalogRequest { Name = Guid.NewGuid().ToString(), RoomId = Guid.NewGuid() };

        var (response, course) = await api.IncludeInCatalog(request);

        ItShouldNotIncludeTheCourse(response);
    }
}