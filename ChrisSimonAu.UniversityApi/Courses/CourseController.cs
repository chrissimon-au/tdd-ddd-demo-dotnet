namespace ChrisSimonAu.UniversityApi.Courses;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    [HttpPost]
    public IActionResult IncludeInCatalog()
    {
        var course = Course.IncludeInCatalog();
        return Created($"http://localhost/courses/{course.Id}", course);
    }
}