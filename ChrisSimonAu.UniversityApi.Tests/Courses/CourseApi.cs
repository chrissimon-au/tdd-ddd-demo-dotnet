namespace ChrisSimonAu.UniversityApi.Tests.Courses;

public class CourseApi
{
    private readonly HttpClient client;

    public CourseApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<(HttpResponseMessage, CourseResponse?)> IncludeInCatalog(IncludeCourseInCatalogRequest request)
    {
        var response = await client.PostAsync("/courses", JsonContent.Create(request));
        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();
        return (response, course);
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