namespace ChrisSimonAu.UniversityApi.Courses;

using Rooms;

public class Course
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public virtual Room? Room { get; set; }
    public static Course? IncludeInCatalog(IncludeCourseInCatalogRequest request, Room? room)
    {
        if (room == null) 
        {
            return null;
        }
        return new Course { Id = Guid.NewGuid(), Name = request.Name, Room = room };
    }

    public CourseResponse ToResponse()
    {
        return new CourseResponse
        {
            Id = this.Id,
            Name = this.Name,
            RoomId = this.Room?.Id,
        };
    }
}