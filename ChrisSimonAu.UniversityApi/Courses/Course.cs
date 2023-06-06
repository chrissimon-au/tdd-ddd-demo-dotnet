namespace ChrisSimonAu.UniversityApi.Courses;

public class Course
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public static Course IncludeInCatalog(IncludeCourseInCatalogRequest request)
    {
        return new Course { Id = Guid.NewGuid(), Name = request.Name };
    }
}