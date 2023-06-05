namespace ChrisSimonAu.UniversityApi.Tests.Courses;

public class CourseApi
{
    private readonly HttpClient client;

    public CourseApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<(HttpResponseMessage, CourseResponse?)> IncludeInCatalog()
    {
        var response = await client.PostAsync("/courses", null);
        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();
        return (response, course);
    }

    public Uri UriForCourseId(Guid? id)
    {
        return new Uri($"http://localhost/courses/{id}");
    }
}