namespace ChrisSimonAu.UniversityApi.Courses;

public class Course
{
    public Guid Id { get; set; }
    public static Course IncludeInCatalog()
    {
        return new Course { Id = Guid.NewGuid() };
    }
}