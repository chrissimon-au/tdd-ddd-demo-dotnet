namespace ChrisSimonAu.UniversityApi.Courses;

using Rooms;

public class Course
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? RoomId { get; set; }
    public static Course? IncludeInCatalog(IncludeCourseInCatalogRequest request, Room? room)
    {
        if (room == null) 
        {
            return null;
        }
        return new Course { Id = Guid.NewGuid(), Name = request.Name, RoomId = request.RoomId };
    }
}