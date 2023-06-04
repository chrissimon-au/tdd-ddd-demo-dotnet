namespace ChrisSimonAu.UniversityApi;
using Microsoft.EntityFrameworkCore;
using Students;

public class UniversityContext : DbContext
{
    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
    }


    public required DbSet<Student> Students {get; set;}
}