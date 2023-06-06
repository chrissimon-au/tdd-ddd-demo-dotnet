namespace ChrisSimonAu.UniversityApi.Courses;

public class CourseResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? RoomId { get; set; }
}