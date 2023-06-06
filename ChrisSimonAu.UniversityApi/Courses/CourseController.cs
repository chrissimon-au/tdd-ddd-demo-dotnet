namespace ChrisSimonAu.UniversityApi.Courses;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    [HttpPost]
    public ActionResult<Course> IncludeInCatalog([FromBody] IncludeCourseInCatalogRequest request)
    {
        var course = Course.IncludeInCatalog(request);
        return Created($"http://localhost/courses/{course.Id}", course);
    }
}