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
    public ActionResult Register([FromBody] RegisterStudentRequest request)
    {
        var student = Student.Register(request);
        context.Students.Add(student);
        context.SaveChanges();
        return Created($"/students/{student.Id}", student);
    }

    [HttpGet]
    [Route("/students/{id}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        var student = context.Students.Find(id);
        return Ok(student);
    }
}