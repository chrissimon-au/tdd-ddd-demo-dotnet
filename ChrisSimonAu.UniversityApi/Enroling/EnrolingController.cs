namespace ChrisSimonAu.UniversityApi.Enroling;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class EnrolingController : ControllerBase
{
    private readonly UniversityContext context;

    public EnrolingController(UniversityContext context)
    {
        this.context = context;
    }

    [HttpPost]
    [Route("/students/{studentId}/courses")]
    public async Task<ActionResult<Enrolment>> EnrolInCourse([FromRoute] Guid studentId, [FromBody] EnrolStudentInCourseRequest request)
    {
        var student = await context.Students.FindAsync(studentId);
        if (student == null)
        {
            return NotFound();
        }

        var course = await context.Courses.FindAsync(request.CourseId);
        if (course == null)
        {
            return BadRequest();
        }

        var numEnrolments = await context.Enrolments.CountAsync(e => e.CourseId == course.Id);
        if (course?.Room?.WouldEnrolmentExceedCapacity(numEnrolments) ?? true)
        {
            return BadRequest();
        }

        var enrolment = new Enrolment { Id = Guid.NewGuid(), StudentId = studentId, CourseId = request.CourseId };
        
        await context.Enrolments.AddAsync(enrolment);
        await context.SaveChangesAsync();
        
        return CreatedAtAction("Get", new { Id = enrolment.Id }, enrolment);
    }

    [HttpGet]
    [Route("/enrolments/{id}")]
    public async Task<ActionResult<Enrolment?>> Get([FromRoute] Guid id)
    {
        return await context.Enrolments.FindAsync(id);
    }
}