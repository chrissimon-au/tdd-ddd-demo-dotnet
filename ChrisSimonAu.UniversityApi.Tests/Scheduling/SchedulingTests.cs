namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

using Microsoft.AspNetCore.Mvc.Testing;
using Rooms;
using Courses;
using Enroling;
using Students;

public class SchedulingTests : IClassFixture<WebApplicationFactory<Program>>
{

    [Fact]
    public async Task GivenThereIsASingleCourseAndRoom_WhenIScheduleCourses()
    {
        var client = new WebApplicationFactory<Program>().CreateClient();
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

    [Fact]
    public async Task GivenThereAreMultipleCoursesAndRooms_WhenIScheduleCourses()
    {
        var client = new WebApplicationFactory<Program>().CreateClient();
        var schedulingApi = new SchedulingApi(client);
        var studentApi = new StudentApi(client);
        var courseApi = new CourseApi(client);
        var roomApi = new RoomApi(client);
        var enrolmentApi = new EnrolmentApi(client);

        var (_, course1) = await courseApi.IncludeInCatalog(new IncludeCourseInCatalogRequest { Name = "First Course" });
        var (_, course2) = await courseApi.IncludeInCatalog(new IncludeCourseInCatalogRequest { Name = "Second Course" });

        var (_, student1) = await studentApi.RegisterStudent(new RegisterStudentRequest { Name = "Student 1" });
        var (_, student2) = await studentApi.RegisterStudent(new RegisterStudentRequest { Name = "Student 2" });
        var (_, student3) = await studentApi.RegisterStudent(new RegisterStudentRequest { Name = "Student 3" });

        await enrolmentApi.EnrolStudentInCourse(student1, course1);
        await enrolmentApi.EnrolStudentInCourse(student2, course1);

        await enrolmentApi.EnrolStudentInCourse(student1, course2);
        await enrolmentApi.EnrolStudentInCourse(student2, course2);
        await enrolmentApi.EnrolStudentInCourse(student3, course2);

        var (_, room1) = await roomApi.SetupRoom(new SetupRoomRequest { Name = "Room1", Capacity = 5 });
        var (_, room2) = await roomApi.SetupRoom(new SetupRoomRequest { Name = "Room2", Capacity = 2 });

        var (response, schedule) = await schedulingApi.Schedule();

        ItShouldScheduleCourseToRoom(schedule, course1!, room2!);
        ItShouldScheduleCourseToRoom(schedule, course2!, room1!);
    }

    private void ItShouldScheduleCourseToRoom(ScheduleResponse? schedule, CourseResponse course, RoomResponse room)
    {
        var scheduledCourses = schedule?.ScheduledCourses;
        Assert.NotNull(scheduledCourses);
        
        var scheduledCourse = scheduledCourses.FirstOrDefault(sc => sc.Id == course?.Id);

        Assert.Equal(course.Name, scheduledCourse?.Name);
        Assert.Equal(room.Id, scheduledCourse?.RoomId);
    }

}