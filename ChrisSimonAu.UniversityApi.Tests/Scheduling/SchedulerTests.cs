namespace ChrisSimonAu.UniversityApi.Tests.Scheduling;

using ChrisSimonAu.UniversityApi.Scheduling;
using ChrisSimonAu.UniversityApi.Courses;
using ChrisSimonAu.UniversityApi.Rooms;

public class SchedulerTests
{
    [Fact]
    public void WhenScheduling()
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
}