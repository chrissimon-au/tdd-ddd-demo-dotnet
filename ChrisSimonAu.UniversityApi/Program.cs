using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ChrisSimonAu.UniversityApi;
using ChrisSimonAu.UniversityApi.Scheduling;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Add services to the container.
services.AddSingleton<DbConnection>(container =>
{
    var connection = new SqliteConnection("DataSource=:memory:");
    connection.Open();

    return connection;
});
services.AddScoped<CourseEnrolmentQuery>();

services.AddDbContext<UniversityContext>((container, options) =>
{
    var connection = container.GetRequiredService<DbConnection>();
    options
        .UseLazyLoadingProxies()
        .UseSqlite(connection);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UniversityContext>();

    db.Database.EnsureCreated();
}

app.Run();

public partial class Program { }
