namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

using ChrisSimonAu.UniversityApi.Scheduling;
using ChrisSimonAu.UniversityApi.Courses;
using ChrisSimonAu.UniversityApi.Rooms;

public class SchedulerTests
{
    private CourseEnrolment NewCourseEnrolment(string name, int EnrolmentCount)
    {
        var course = Course.IncludeInCatalog(new IncludeCourseInCatalogRequest { Name = name });
        return new CourseEnrolment { Course = course, EnrolmentCount = EnrolmentCount };
    }

    [Fact]
    public void GivenSingleCourseSingleRoom_whenScheduling()
    {
        var scheduler = new Scheduler();

        var courseEnrolments = new List<CourseEnrolment> { NewCourseEnrolment("Test Course", 2) };

        var room = Room.Setup(new SetupRoomRequest { Name = "Test Room", Capacity = 2 });
        var rooms = new List<Room> { room };

        var schedule = Scheduler.ScheduleCourses(courseEnrolments, rooms);

        var scheduledCourses = schedule.Courses;

        Assert.Single(scheduledCourses);

        var scheduledCourse = scheduledCourses.Single();
        
        Assert.Equal(room, scheduledCourse.Room);
    }

    [Fact]
    public void GivenTwoCoursesTwoRooms_whenScheduling()
    {
        var scheduler = new Scheduler();

        var courseEnrolment1 = NewCourseEnrolment("First Course", 4);
        var courseEnrolment2 = NewCourseEnrolment("Second Course", 2);
        var courseEnrolments = new List<CourseEnrolment> { courseEnrolment1, courseEnrolment2 };

        var room1 = Room.Setup(new SetupRoomRequest { Name = "Room1", Capacity = 2 });
        var room2 = Room.Setup(new SetupRoomRequest { Name = "Room2", Capacity = 4 });
        var rooms = new List<Room> { room1, room2 };

        var schedule = Scheduler.ScheduleCourses(courseEnrolments, rooms);

        var scheduledCourses = schedule.Courses;

        Assert.Equal(2, scheduledCourses.Count());

        var scheduledCourse1 = scheduledCourses.Single(c => c.Id == courseEnrolment1.Course.Id);
        var scheduledCourse2 = scheduledCourses.Single(c => c.Id == courseEnrolment2.Course.Id);

        Assert.Equal(room2.Id, scheduledCourse1?.Room?.Id);
        Assert.Equal(room1.Id, scheduledCourse2?.Room?.Id);
    }
}