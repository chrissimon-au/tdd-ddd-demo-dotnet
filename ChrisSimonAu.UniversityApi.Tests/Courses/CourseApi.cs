namespace ChrisSimonAu.UniversityApi.Tests.Courses;

using Rooms;

public class CourseApi
{
    private readonly HttpClient client;
    private readonly RoomApi roomApi;

    public CourseApi(HttpClient client)
    {
        this.client = client;
        this.roomApi = new RoomApi(client);
    }

    public async Task<(HttpResponseMessage, CourseResponse?)> IncludeInCatalog(IncludeCourseInCatalogRequest courseRequest)
    {
        var response = await client.PostAsync("/courses", JsonContent.Create(courseRequest));
        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();
        return (response, course);
    }

    public async Task<(HttpResponseMessage, CourseResponse?)> IncludeInCatalog(IncludeCourseInCatalogRequest courseRequest, SetupRoomRequest roomRequest)
    {
        var (_, room) = await roomApi.SetupRoom(roomRequest);
        courseRequest.RoomId = room?.Id;
        return await IncludeInCatalog(courseRequest);
    }

    public Uri UriForCourseId(Guid? id)
    {
        return new Uri($"http://localhost/courses/{id}");
    }

    public async Task<(HttpResponseMessage, CourseResponse?)> GetCourse(Uri? courseLocation)
    {
        var response = await client.GetAsync(courseLocation);
        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();
        return (response, course);
    }
}