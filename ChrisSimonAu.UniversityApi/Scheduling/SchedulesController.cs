namespace ChrisSimonAu.UniversityApi.Scheduling;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Courses;

[ApiController]
[Route("[controller]")]
public class SchedulesController : ControllerBase
{
    private UniversityContext context;

    public SchedulesController(UniversityContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult<ScheduleResponse>> Schedule()
    {
        var courseEnrolments = await (
            from enrolment in context.Enrolments
            join course in context.Courses on enrolment.Course!.Id equals course.Id
            group enrolment by course into ces
            select new CourseEnrolment { Course = ces.Key, EnrolmentCount = ces.Count() }
        ).ToListAsync();

        var rooms = await context.Rooms.ToListAsync();

        var proposedSchedule = Scheduler.ScheduleCourses(courseEnrolments, rooms);

        return Ok(new ScheduleResponse() {
            ScheduledCourses = proposedSchedule.Courses.Select(c => c.ToResponse())
        });
    }
}