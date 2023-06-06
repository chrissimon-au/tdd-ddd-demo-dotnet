namespace ChrisSimonAu.UniversityApi.Courses;

using Rooms;
using Enroling;
using Students;

public class Course
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public virtual Room? Room { get; set; }
    public virtual IEnumerable<Enrolment>? Enrolments { get; set; }
    public static Course? IncludeInCatalog(IncludeCourseInCatalogRequest request, Room? room)
    {
        if (room == null) 
        {
            return null;
        }
        return new Course { Id = Guid.NewGuid(), Name = request.Name, Room = room };
    }

    public Enrolment? Enrol(Student student)
    {
        if (Enrolments?.Count() + 1 > Room?.Capacity)
        {
            return null;
        }
        return Enrolment.StudentEnroled(student, this);
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