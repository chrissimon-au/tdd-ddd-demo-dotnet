namespace ChrisSimonAu.UniversityApi.Scheduling;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class SchedulesController : ControllerBase
{
    private UniversityContext context;
    private readonly CourseEnrolmentQuery courseEnrolmentQuery;

    public SchedulesController(UniversityContext context,
        CourseEnrolmentQuery courseEnrolmentQuery)
    {
        this.context = context;
        this.courseEnrolmentQuery = courseEnrolmentQuery;
    }

    [HttpPost]
    public async Task<ActionResult<ScheduleResponse>> Schedule()
    {
        var courseEnrolments = await courseEnrolmentQuery.GetCourseEnrolments();
        var rooms = await context.Rooms.ToListAsync();

        var proposedSchedule = Scheduler.ScheduleCourses(courseEnrolments, rooms);

        return Ok(new ScheduleResponse() {
            ScheduledCourses = proposedSchedule.Courses.Select(c => c.ToResponse())
        });
    }
}