namespace ChrisSimonAu.UniversityApi.Rooms;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly UniversityContext context;

    public RoomsController(UniversityContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Room>> Setup([FromBody] SetupRoomRequest request)
    {
        var room = Room.Setup(request);
        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();
        return CreatedAtAction("Get", new { Id = room.Id }, room);
    }

    [HttpGet]
    [Route("/rooms/{id}")]
    public async Task<ActionResult<Room>> Get([FromRoute] Guid id)
    {
        var room = await context.Rooms.FindAsync(id);
        return room == null ? NotFound() : Ok(room);
    }
}