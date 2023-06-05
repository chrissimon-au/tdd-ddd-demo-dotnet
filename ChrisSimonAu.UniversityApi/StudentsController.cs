namespace ChrisSimonAu.UniversityApi;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    [HttpPost]
    public ActionResult Register()
    {
        var student = Student.Register();
        return Created($"/students/{student.Id}", student);
    }
}