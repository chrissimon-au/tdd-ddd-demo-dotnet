namespace ChrisSimonAu.UniversityApi;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    [HttpPost]
    public ActionResult Register([FromBody] RegisterStudentRequest request)
    {
        var student = Student.Register(request);
        return Created($"/students/{student.Id}", student);
    }

    [HttpGet]
    [Route("/students/{id}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        return Ok();
    }
}