namespace ChrisSimonAu.UniversityApi.Scheduling;

using Courses;

public class Schedule
{
    public required IList<Course> Courses { get; set; }
}