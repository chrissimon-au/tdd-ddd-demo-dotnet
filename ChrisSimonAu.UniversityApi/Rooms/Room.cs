namespace ChrisSimonAu.UniversityApi.Rooms;

public class Room
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public int Capacity { get; private set; }

    public static Room Setup(SetupRoomRequest request)
    {
        return new Room { Id = Guid.NewGuid(), Name = request.Name, Capacity = request.Capacity };
    }

    public bool CanCourseFit(int numEnrolments)
    {
        return numEnrolments <= Capacity;
    }
}