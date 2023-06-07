namespace ChrisSimonAu.UniversityApi.Courses;

using Rooms;
using Enroling;
using Students;

public class Course
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public virtual Room? Room { get; private set; }
    public virtual IEnumerable<Enrolment>? Enrolments { get; private set; }
    
    public static Course IncludeInCatalog(IncludeCourseInCatalogRequest request)
    {
        return new Course { Id = Guid.NewGuid(), Name = request.Name };
    }

    public Enrolment? Enrol(Student student)
    {
        return Enrolment.StudentEnroled(student, this);
    }

    public bool AssignTo(Room? room)
    {
        Room = room;
        return room != null;
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