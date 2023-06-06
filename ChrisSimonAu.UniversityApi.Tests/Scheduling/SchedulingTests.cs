namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

using Microsoft.AspNetCore.Mvc.Testing;
using Rooms;
using Courses;
using Enroling;
using Students;

public class SchedulingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SchedulingTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenIAmAnAdmin_WhenIScheduleCourses()
    {
        var client = _factory.CreateClient();
        var schedulingApi = new SchedulingApi(client);
        var studentApi = new StudentApi(client);
        var courseApi = new CourseApi(client);
        var roomApi = new RoomApi(client);
        var enrolmentApi = new EnrolmentApi(client);

        var (_, student) = await studentApi.RegisterStudent(new RegisterStudentRequest { Name = "Test Student" });
        var (_, course) = await courseApi.IncludeInCatalog(new IncludeCourseInCatalogRequest { Name = "Test Course" });
        await enrolmentApi.EnrolStudentInCourse(student, course);

        var (_, room) = await roomApi.SetupRoom(new SetupRoomRequest { Name = "Test Room", Capacity = 1 });

        var (response, schedule) = await schedulingApi.Schedule();

        ItShouldScheduleTheCourses(response);

        ItShouldListTheScheduledCourses(schedule, course, room);
    }

    private void ItShouldScheduleTheCourses(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    private void ItShouldListTheScheduledCourses(ScheduleResponse? schedule, CourseResponse? course, RoomResponse? room)
    {
        var scheduledCourses = schedule?.ScheduledCourses;
        Assert.NotNull(scheduledCourses);
        Assert.Single(scheduledCourses);

        var scheduledCourse = scheduledCourses.Single();
        Assert.Equal(course?.Id, scheduledCourse.Id);
        Assert.Equal(course?.Name, scheduledCourse.Name);
        Assert.Equal(room?.Id, scheduledCourse?.RoomId);
    }

}