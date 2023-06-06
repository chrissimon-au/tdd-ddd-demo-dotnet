namespace ChrisSimonAu.UniversityApi.Scheduling;

using Courses;

public class ScheduleResponse
{
    public IEnumerable<CourseResponse>? ScheduledCourses { get; set; }
}