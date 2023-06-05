namespace ChrisSimonAu.UniversityApi;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    [HttpPost]
    public ActionResult Register()
    {
        return Created("", Student.Register());
    }
}