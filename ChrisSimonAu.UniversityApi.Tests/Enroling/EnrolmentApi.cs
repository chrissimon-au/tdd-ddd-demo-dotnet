namespace ChrisSimonAu.UniversityApi.Tests.Enroling;

using Students;
using Courses;

public class EnrolmentApi
{
    private readonly HttpClient client;

    public EnrolmentApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<(HttpResponseMessage, EnrolmentResponse?)> EnrolStudentInCourse(StudentResponse? student, CourseResponse? course)
    {
        var request = new EnrolStudentInCourseRequest { CourseId = course?.Id };
        var response = await client.PostAsync($"/students/{student?.Id}/courses", JsonContent.Create(request));
        var enrolment = await response.Content.ReadFromJsonAsync<EnrolmentResponse>();
        return (response, enrolment);
    }
}