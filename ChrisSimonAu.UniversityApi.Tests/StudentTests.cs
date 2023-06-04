namespace ChrisSimonAu.UniversityApi.Tests;

using Microsoft.AspNetCore.Mvc.Testing;

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

        var response = await client.PostAsync("/students", null);

        ItShouldRegisterANewStudent(response);
    }

    private void ItShouldRegisterANewStudent(HttpResponseMessage response)
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}