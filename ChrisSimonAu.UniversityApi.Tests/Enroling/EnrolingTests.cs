namespace ChrisSimonAu.UniversityApi.Tests.Enroling;

using Microsoft.AspNetCore.Mvc.Testing;
using Courses;
using Students;

public class EnrolingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public EnrolingTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenIAmARegisteredStudent_WhenIEnrolInACourse()
    {
        var client = _factory.CreateClient();
        var courseApi = new CourseApi(client);
        var studentApi = new StudentApi(client);
        var enrolmentApi = new EnrolmentApi(client);

        var courseRequest = new IncludeCourseInCatalogRequest { Name = "Test Course" };

        var (response, course) = await courseApi.IncludeInCatalog(courseRequest);

        var studentRequest = new RegisterStudentRequest { Name = "Test student" };
        var (_, student) = await studentApi.RegisterStudent(studentRequest);

        var (enrolmentResponse, enrolment) = await enrolmentApi.EnrolStudentInCourse(student, course);

        ItShouldEnrolMe(enrolmentResponse);
        ItShouldAllocateANewEnrolmentId(enrolment);
        ItShouldConfirmEnrolmentDetails(enrolment, student, course);
        ItShouldShowWhereToCheckEnrolmentDetails(enrolmentResponse, enrolment);
    }

    private void ItShouldEnrolMe(HttpResponseMessage enrolmentResponse)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, enrolmentResponse.StatusCode);
    }

    private void ItShouldAllocateANewEnrolmentId(EnrolmentResponse? enrolment)
    {
        Assert.NotNull(enrolment);
        Assert.NotEqual(Guid.Empty, enrolment.Id);
    }

    private void ItShouldConfirmEnrolmentDetails(EnrolmentResponse? enrolment, StudentResponse? student, CourseResponse? course)
    {
        Assert.Equal(student?.Id, enrolment?.StudentId);
        Assert.Equal(course?.Id, enrolment?.CourseId);
    }

    private void ItShouldShowWhereToCheckEnrolmentDetails(HttpResponseMessage response, EnrolmentResponse? enrolment)
    {
        Assert.Equal(new Uri($"http://localhost/enrolments/{enrolment?.Id}"), response.Headers.Location);
    }

    [Fact]
    public async Task GivenIHaveTheWrongStudentId_WhenIEnrolInACourse()
    {
        var client = _factory.CreateClient();
        var courseApi = new CourseApi(client);
        var enrolmentApi = new EnrolmentApi(client);

        var courseRequest = new IncludeCourseInCatalogRequest { Name = "Test Course" };

        var (response, course) = await courseApi.IncludeInCatalog(courseRequest);

        var student = new StudentResponse { Id = Guid.NewGuid() };

        var (enrolmentResponse, _) = await enrolmentApi.EnrolStudentInCourse(student, course);

        ItShouldNotEnrolMe(enrolmentResponse);
    }

    private void ItShouldNotEnrolMe(HttpResponseMessage enrolmentResponse)
    {
        Assert.Equal(System.Net.HttpStatusCode.NotFound, enrolmentResponse.StatusCode);
    }

    [Fact]
    public async Task GivenIHaveTheWrongCourseId_WhenIEnrolInACourse()
    {
        var client = _factory.CreateClient();
        var studentApi = new StudentApi(client);
        var enrolmentApi = new EnrolmentApi(client);

        var studentRequest = new RegisterStudentRequest { Name = "Test student" };
        var (_, student) = await studentApi.RegisterStudent(studentRequest);

        var course = new CourseResponse { Id = Guid.NewGuid() };

        var (enrolmentResponse, _) = await enrolmentApi.EnrolStudentInCourse(student, course);

        ItShouldNotEnrolMeWithErrors(enrolmentResponse);
    }

    private void ItShouldNotEnrolMeWithErrors(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GivenIHaveEnroled_WhenICheckMyEnrolment()
    {
        var client = _factory.CreateClient();
        var courseApi = new CourseApi(client);
        var studentApi = new StudentApi(client);
        var enrolmentApi = new EnrolmentApi(client);

        var courseRequest = new IncludeCourseInCatalogRequest { Name = "Test Course" };

        var (_, course) = await courseApi.IncludeInCatalog(courseRequest);

        var studentRequest = new RegisterStudentRequest { Name = "Test student" };
        var (_, student) = await studentApi.RegisterStudent(studentRequest);

        var (response, _) = await enrolmentApi.EnrolStudentInCourse(student, course);

        var (checkedResponse, checkedEnrolment) = await enrolmentApi.GetEnrolment(response.Headers.Location);

        ItShouldFindTheEnrolment(checkedResponse);
        ItShouldConfirmEnrolmentDetails(checkedEnrolment, student, course);
    }

    private void ItShouldFindTheEnrolment(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}