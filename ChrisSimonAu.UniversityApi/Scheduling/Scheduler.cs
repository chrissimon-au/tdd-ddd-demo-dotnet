namespace ChrisSimonAu.UniversityApi.Scheduling;

using ChrisSimonAu.UniversityApi.Rooms;
using Courses;

public class Scheduler
{
    private static Comparison<CourseEnrolment> ByMostPopular = (ce1, ce2) => ce2.EnrolmentCount.CompareTo(ce1.EnrolmentCount);
    private static Comparison<Room> ByMostSpacious =  (r1, r2) => r2.Capacity.CompareTo(r1.Capacity);

    public static Schedule ScheduleCourses(List<CourseEnrolment> courseEnrolments, List<Room> rooms)
    {
        var schedule = new Schedule();

        courseEnrolments.Sort(ByMostPopular);
        rooms.Sort(ByMostSpacious);
        
        foreach (var (courseEnrolment, room) in courseEnrolments.Zip(rooms))
        {
            var course = courseEnrolment.Course;
            course.AssignTo(room);
            schedule.Courses.Add(course);
        }
        return schedule;
    }
}