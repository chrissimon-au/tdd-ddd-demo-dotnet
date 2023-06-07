namespace ChrisSimonAu.UniversityApi.Scheduling;

using ChrisSimonAu.UniversityApi.Rooms;
using Courses;

public class Scheduler
{
    public static Schedule ScheduleCourses(List<CourseEnrolment> courseEnrolments, List<Room> rooms)
    {
        var schedule = new Schedule()
        {
            Courses = new List<Course>()
        };

        courseEnrolments.Sort((ce1, ce2) => ce2.EnrolmentCount.CompareTo(ce1.EnrolmentCount));
        rooms.Sort((r1, r2) => r2.Capacity.CompareTo(r1.Capacity));
        
        foreach (var courseEnrolmentRoom in courseEnrolments.Zip(rooms))
        {
            var course = courseEnrolmentRoom.First.Course;
            course.Room = courseEnrolmentRoom.Second;
            schedule.Courses.Add(course);
        }
        return schedule;
    }
}