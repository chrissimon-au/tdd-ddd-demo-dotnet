namespace ChrisSimonAu.UniversityApi.Tests.Students;

public class StudentApi
{
    private readonly HttpClient client;

    public StudentApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<(HttpResponseMessage, StudentResponse?)> RegisterStudent(RegisterStudentRequest registerStudent)
    {
        var response = await client.PostAsync("/students", JsonContent.Create(registerStudent));
        var student = await response.Content.ReadFromJsonAsync<StudentResponse>();
        return (response, student);
    }

    public Uri UriForStudentId(Guid? studentId)
        => new Uri($"http://localhost/students/{studentId}");

    public async Task<(HttpResponseMessage, StudentResponse?)> GetStudent(Uri? newStudentLocation)
    {
        var response = await client.GetAsync(newStudentLocation);
        var student = await response.Content.ReadFromJsonAsync<StudentResponse>();
        return (response, student);
    }

    public async Task<(HttpResponseMessage, StudentResponse?)> GetStudent(Guid studentId)
        => await GetStudent(UriForStudentId(studentId));
}