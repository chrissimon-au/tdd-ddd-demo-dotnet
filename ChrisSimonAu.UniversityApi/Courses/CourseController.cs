namespace ChrisSimonAu.UniversityApi.Courses;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly UniversityContext context;

    public CoursesController(UniversityContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Course>> IncludeInCatalog([FromBody] IncludeCourseInCatalogRequest request)
    {
        var course = Course.IncludeInCatalog(request);
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();
        return Created($"http://localhost/courses/{course.Id}", course);
    }

    [HttpGet]
    [Route("/courses/{id}")]
    public async Task<ActionResult<Course>> Get([FromRoute] Guid id)
    {
        var course = await context.Courses.FindAsync(id);
        return Ok(course);
    }
}