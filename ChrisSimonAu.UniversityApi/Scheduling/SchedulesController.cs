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
        var room = await this.context.Rooms.SingleAsync();
        var course = await this.context.Courses.SingleAsync();
        course.Room = room;
        return Ok(new ScheduleResponse() {
            ScheduledCourses = new List<CourseResponse>() { course.ToResponse() }
        });
    }
}