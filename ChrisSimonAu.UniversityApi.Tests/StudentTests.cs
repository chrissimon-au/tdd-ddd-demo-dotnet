namespace ChrisSimonAu.UniversityApi.Tests;

using Microsoft.AspNetCore.Mvc.Testing;
using System;

public class StudentTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public StudentTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private StudentApi CreateStudentApi() => new StudentApi(_factory.CreateClient());

    [Fact]
    public async Task GivenIAmAStudent_WhenIRegister() 
    {
        var api = CreateStudentApi();

        var registerStudent = new RegisterStudentRequest { Name = Guid.NewGuid().ToString() };

        var (response, student) = await api.RegisterStudent(registerStudent);
        
        ItShouldRegisterANewStudent(response);
        ItShouldAllocateANewId(student);
        ItShouldShowWhereToLocateNewStudent(response, student);
        ItShouldConfirmStudentDetails(registerStudent, student);
    }

    private void ItShouldRegisterANewStudent(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    private void ItShouldAllocateANewId(StudentResponse? student)
    {
        Assert.NotNull(student);
        Assert.NotEqual(Guid.Empty, student.Id);
    }

    private void ItShouldShowWhereToLocateNewStudent(HttpResponseMessage response, StudentResponse? student)
    {
        var location = response.Headers.Location;
        Assert.NotNull(location);
        Assert.Equal($"/students/{student!.Id}", location.ToString());
    }

    private void ItShouldConfirmStudentDetails(RegisterStudentRequest request, StudentResponse? response)
    {
        Assert.Equal(request.Name, response!.Name);
    }

    [Theory()]
    [InlineData("Test Student")]
    [InlineData("Another Student")]
    public async Task GivenIHaveRegistered_WhenICheckMyDetails(string studentName)
    {
        var api = CreateStudentApi();

        var registerStudent = new RegisterStudentRequest { Name = studentName };

        var (response, _) = await api.RegisterStudent(registerStudent);
        
        var newStudentLocation = response.Headers.Location;

        var (checkedStudentResponse, checkedStudent) = await api.GetStudent(newStudentLocation);

        ItShouldFindTheNewStudent(checkedStudentResponse);
        ItShouldConfirmStudentDetails(registerStudent, checkedStudent);
    }

    private void ItShouldFindTheNewStudent(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}