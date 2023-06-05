namespace ChrisSimonAu.UniversityApi.Rooms;

public class Room
{
    public Guid Id { get; set; }

    public static Room Setup()
    {
        return new Room { Id = Guid.NewGuid() };
    }
}