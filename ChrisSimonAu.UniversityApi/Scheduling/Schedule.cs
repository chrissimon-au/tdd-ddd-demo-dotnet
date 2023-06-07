namespace ChrisSimonAu.UniversityApi.Scheduling;

using Courses;

public class Schedule
{
    public IList<Course> Courses { get; set; } = new List<Course>();
}