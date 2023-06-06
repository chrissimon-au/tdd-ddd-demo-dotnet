namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

using ChrisSimonAu.UniversityApi.Scheduling;
using ChrisSimonAu.UniversityApi.Courses;
using ChrisSimonAu.UniversityApi.Rooms;

public class SchedulerTests
{
    [Fact]
    public void GivenSingleCourseSingleRoom_whenScheduling()
    {
        var scheduler = new Scheduler();

        var course = Course.IncludeInCatalog(new IncludeCourseInCatalogRequest { Name = "Test Course" });
        var courseEnrolment = new CourseEnrolment { Course = course, EnrolmentCount = 2 };
        var courseEnrolments = new List<CourseEnrolment> { courseEnrolment };
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

        var course1 = Course.IncludeInCatalog(new IncludeCourseInCatalogRequest { Name = "First Course" });
        var courseEnrolment1 = new CourseEnrolment { Course = course1, EnrolmentCount = 4 };
        var course2 = Course.IncludeInCatalog(new IncludeCourseInCatalogRequest { Name = "Second Course" });
        var courseEnrolment2 = new CourseEnrolment { Course = course2, EnrolmentCount = 2 };
        var courseEnrolments = new List<CourseEnrolment> { courseEnrolment1, courseEnrolment2 };

        var room1 = Room.Setup(new SetupRoomRequest { Name = "Room1", Capacity = 2 });
        var room2 = Room.Setup(new SetupRoomRequest { Name = "Room2", Capacity = 4 });
        var rooms = new List<Room> { room1, room2 };

        var schedule = Scheduler.ScheduleCourses(courseEnrolments, rooms);

        var scheduledCourses = schedule.Courses;

        Assert.Equal(2, scheduledCourses.Count());

        var scheduledCourse1 = scheduledCourses.Single(c => c.Id == course1.Id);
        var scheduledCourse2 = scheduledCourses.Single(c => c.Id == course2.Id);

        Assert.Equal(room2.Id, scheduledCourse1?.Room?.Id);
        Assert.Equal(room1.Id, scheduledCourse2?.Room?.Id);
    }
}