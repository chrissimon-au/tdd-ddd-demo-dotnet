namespace ChrisSimonAu.UniversityApi.Scheduling;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SchedulesController : ControllerBase
{
    [HttpPost]
    public IActionResult Schedule()
    {
        return Ok();
    }
}