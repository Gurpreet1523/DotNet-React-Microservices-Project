using Microsoft.EntityFrameworkCore;
using Portfolio.Projects.API.Data;
using Portfolio.Projects.API.Interfaces;
using Portfolio.Projects.API.Repositories;
using Portfolio.Projects.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration
            .GetConnectionString("ProjectDb")));

builder.Services.AddScoped<
    IProjectRepository,
    ProjectRepository>();

builder.Services.AddScoped<
    IProjectService,
    ProjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projects API v1");
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





