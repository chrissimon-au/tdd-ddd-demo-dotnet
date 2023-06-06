namespace ChrisSimonAu.UniversityApi.Courses;

public class IncludeCourseInCatalogRequest
{
    public string? Name { get; set; }
    public Guid RoomId { get; set; }
}