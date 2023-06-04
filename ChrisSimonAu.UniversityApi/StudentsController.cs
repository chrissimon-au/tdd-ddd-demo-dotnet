namespace ChrisSimonAu.UniversityApi;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly UniversityContext context;

    public StudentsController(UniversityContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Register([FromBody] RegisterStudentRequest request)
    {
        var student = Student.Register(request);
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
        return CreatedAtAction("Get", new { Id = student.Id }, student);
    }

    [HttpGet]
    [Route("/students/{id}")]
    public async Task<ActionResult<Student>> Get([FromRoute] Guid id)
    {
        var student = await context.Students.FindAsync(id);
        return Ok(student);
    }
}