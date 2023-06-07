namespace ChrisSimonAu.UniversityApi.Scheduling;

using ChrisSimonAu.UniversityApi.Rooms;
using Courses;

public class Scheduler
{
    private static Comparison<CourseEnrolment> ByLeastPopular = (ce1, ce2) => ce1.EnrolmentCount.CompareTo(ce2.EnrolmentCount);
    private static Comparison<Room> ByLeastSpacious =  (r1, r2) => r1.Capacity.CompareTo(r2.Capacity);

    public static Schedule ScheduleCourses(List<CourseEnrolment> courseEnrolments, List<Room> rooms)
    {
        var schedule = new Schedule();

        courseEnrolments.Sort(ByLeastPopular);
        rooms.Sort(ByLeastSpacious);
        
        foreach (var courseEnrolment in courseEnrolments)
        {
            var course = courseEnrolment.Course;
            var candidateRoom = rooms.FirstOrDefault(r => r.CanCourseFit(courseEnrolment.EnrolmentCount));
            if (course.AssignTo(candidateRoom))
            {
                schedule.Courses.Add(course);
                rooms.Remove(candidateRoom!);
            }
        }
        return schedule;
    }
}