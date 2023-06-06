namespace ChrisSimonAu.UniversityApi.Tests.Enroling;

using Microsoft.AspNetCore.Mvc.Testing;
using Rooms;
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

        var roomRequest = new SetupRoomRequest { Name = "Test Room", Capacity = 5 };
        var courseRequest = new IncludeCourseInCatalogRequest { Name = "Test Course" };

        var (response, course) = await courseApi.IncludeInCatalog(courseRequest, roomRequest);

        var studentRequest = new RegisterStudentRequest { Name = "Test student" };
        var (_, student) = await studentApi.RegisterStudent(studentRequest);

        var (enrolmentResponse, enrolment) = await enrolmentApi.EnrolStudentInCourse(student, course);

        ItShouldEnrolMe(enrolmentResponse);
        ItShouldAllocateANewEnrolmentId(enrolment);
        ItShouldConfirmEnrolmentDetails(enrolment, student, course);
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
}