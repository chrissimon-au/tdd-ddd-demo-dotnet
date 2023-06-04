namespace ChrisSimonAu.UniversityApi.Rooms;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    [HttpPost]
    public IActionResult Setup()
    {
        return Created("", null);
    }
}