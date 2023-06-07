namespace ChrisSimonAu.UniversityApi.Scheduling;

using Microsoft.EntityFrameworkCore;

public class CourseEnrolmentQuery
{
    private readonly UniversityContext context;

    public CourseEnrolmentQuery(UniversityContext context)
    {
        this.context = context;
    }

    public async Task<List<CourseEnrolment>> GetCourseEnrolments()
    {
        return await (
            from enrolment in context.Enrolments
            join course in context.Courses on enrolment.Course!.Id equals course.Id
            group enrolment by course into ces
            select new CourseEnrolment { Course = ces.Key, EnrolmentCount = ces.Count() }
        ).ToListAsync();
    }
}