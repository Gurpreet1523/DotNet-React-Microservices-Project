using Microsoft.EntityFrameworkCore;
using Portfolio.Experience.API.Data;
using Portfolio.Experience.API.Interfaces;
using Portfolio.Experience.API.Repositories;
using Portfolio.Experience.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<ExperienceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString("ExperienceDb")));

builder.Services.AddScoped<
    IExperiencesRepository,
    ExperiencesRepository>();

builder.Services.AddScoped<
    IExperienceService,
    ExperienceService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();