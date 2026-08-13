using Portfolio.Profile.API.Data;
using Portfolio.Profile.API.Interfaces;
using Portfolio.Profile.API.Repositories;
using Portfolio.Profile.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProfileDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration
            .GetConnectionString("ProfileDb")));

builder.Services.AddScoped<
    IProfileRepository,
    ProfileRepository>();

builder.Services.AddScoped<
    IProfileService,
    ProfileService>();

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

app.Run();





