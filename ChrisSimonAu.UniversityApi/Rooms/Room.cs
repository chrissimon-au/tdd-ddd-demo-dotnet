namespace ChrisSimonAu.UniversityApi.Rooms;

public class Room
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public int Capacity { get; set; }

    public static Room Setup(SetupRoomRequest request)
    {
        return new Room { Id = Guid.NewGuid(), Name = request.Name, Capacity = request.Capacity };
    }
}