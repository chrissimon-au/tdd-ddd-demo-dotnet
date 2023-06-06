namespace ChrisSimonAu.UniversityApi.Enroling;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class EnrolingController : ControllerBase
{
    [HttpPost]
    [Route("/students/{studentId}/courses")]
    public IActionResult EnrolInCourse([FromRoute] Guid studentId, [FromBody] EnrolStudentInCourseRequest request)
    {
        return Created("", null);
    }
}