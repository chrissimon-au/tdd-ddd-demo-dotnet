namespace ChrisSimonAu.UniversityApi;

using System;

public class Student
{
    public Guid Id {get; set;}

    public string? Name {get; set;}

    public static Student Register(RegisterStudentRequest request)
    {
        return new Student { Id = Guid.NewGuid(), Name = request.Name };
    }
}