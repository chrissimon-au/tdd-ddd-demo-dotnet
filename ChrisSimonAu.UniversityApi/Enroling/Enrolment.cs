namespace ChrisSimonAu.UniversityApi.Enroling;

using Students;
using Courses;

public class Enrolment
{
    public Guid? Id { get; private set; }
    public virtual Student? Student { get; private set; }
    public virtual Course? Course { get; private set; }

    public static Enrolment StudentEnroled(Student student, Course course)
    {
        return new Enrolment { Id = Guid.NewGuid(), Student = student, Course = course };
    }

    public EnrolmentResponse ToResponse()
    {
        return new EnrolmentResponse { Id = Id, StudentId = Student?.Id, CourseId = Course?.Id };
    }
}