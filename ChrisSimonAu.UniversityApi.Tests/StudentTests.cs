namespace ChrisSimonAu.UniversityApi.Tests;

using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http.Json;

public class StudentTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public StudentTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenIAmAStudent_WhenIRegister() 
    {
        var client = _factory.CreateClient();

        var registerStudent = new RegisterStudentRequest { Name = Guid.NewGuid().ToString() };

        var response = await client.PostAsync("/students", JsonContent.Create(registerStudent));
        var student = await response.Content.ReadFromJsonAsync<StudentResponse>();
        
        ItShouldRegisterANewStudent(response);
        ItShouldAllocateANewId(response, student);
        ItShouldShowWhereToLocateNewStudent(response, student);
        ItShouldConfirmStudentDetails(registerStudent, student);
    }

    private void ItShouldRegisterANewStudent(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    private void ItShouldAllocateANewId(HttpResponseMessage response, StudentResponse? student)
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

    [Fact]
    public async Task GivenIHaveRegistered_WhenICheckMyDetails()
    {
        var client = _factory.CreateClient();

        var registerStudent = new RegisterStudentRequest { Name = "Test Student" };

        var response = await client.PostAsync("/students", JsonContent.Create(registerStudent));
        var newStudentLocation = response.Headers.Location;

        var checkedStudentResponse = await client.GetAsync(newStudentLocation);

        ItShouldFindTheNewStudent(checkedStudentResponse);
    }

    private void ItShouldFindTheNewStudent(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}