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
        
        return Created("", new Enrolment { Id = Guid.NewGuid(), StudentId = studentId, CourseId = request.CourseId });
    }
}