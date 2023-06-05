namespace ChrisSimonAu.UniversityApi.Rooms;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    [HttpPost]
    public IActionResult Setup()
    {
        var room = Room.Setup();
        return Created($"http://localhost/rooms/{room.Id}", room);
    }
}