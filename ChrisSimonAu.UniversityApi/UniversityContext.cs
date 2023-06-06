namespace ChrisSimonAu.UniversityApi;
using Microsoft.EntityFrameworkCore;
using Students;
using Rooms;
using Courses;

public class UniversityContext : DbContext
{
    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
    }


    public required DbSet<Student> Students {get; set;}
    public required DbSet<Room> Rooms {get; set;}
    public required DbSet<Course> Courses {get; set;}
}