namespace ChrisSimonAu.UniversityApi.Scheduling;

using Courses;

public class CourseEnrolment
{
    public required Course Course { get; set; }
    public int EnrolmentCount { get; set; }
}