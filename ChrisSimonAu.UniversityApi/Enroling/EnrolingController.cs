namespace ChrisSimonAu.UniversityApi.Enroling;

using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<EnrolmentResponse>> EnrolInCourse([FromRoute] Guid studentId, [FromBody] EnrolStudentInCourseRequest request)
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

        var enrolment = course.Enrol(student);
        
        if (enrolment == null)
        {
            return BadRequest();
        }
        
        await context.Enrolments.AddAsync(enrolment);
        await context.SaveChangesAsync();
        
        return CreatedAtAction("Get", new { Id = enrolment.Id }, enrolment.ToResponse());
    }

    [HttpGet]
    [Route("/enrolments/{id}")]
    public async Task<ActionResult<EnrolmentResponse?>> Get([FromRoute] Guid id)
    {
        return (await context.Enrolments.FindAsync(id))?.ToResponse();
    }
}