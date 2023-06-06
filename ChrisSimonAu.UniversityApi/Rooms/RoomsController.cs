namespace ChrisSimonAu.UniversityApi.Rooms;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    [HttpPost]
    public IActionResult Setup([FromBody] SetupRoomRequest request)
    {
        var room = Room.Setup(request);
        return CreatedAtAction("Get", new { Id = room.Id }, room);
    }

    [HttpGet]
    [Route("/rooms/{id}")]
    public IActionResult Get()
    {
        return Ok();
    }
}