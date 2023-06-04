namespace ChrisSimonAu.UniversityApi.Tests.Students;

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
        ItShouldShowWhereToLocateNewStudent(response, api.UriForStudentId(student?.Id));
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

    private void ItShouldShowWhereToLocateNewStudent(HttpResponseMessage response, Uri studentUri)
    {
        var location = response.Headers.Location;
        Assert.NotNull(location);
        Assert.Equal(studentUri, location);
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

    [Fact]
    public async Task GivenIHaveTheWrongId_WhenICheckMyDetails()
    {
        var api = CreateStudentApi();

        Guid wrongId = Guid.NewGuid();

        var (response, _) = await api.GetStudent(wrongId);

        ItShouldNotFindTheStudent(response);
    }

    private void ItShouldNotFindTheStudent(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
}