using Microsoft.EntityFrameworkCore;
using Portfolio.Contact.API.Data;
using Portfolio.Contact.API.Interfaces;
using Portfolio.Contact.API.Repositories;
using Portfolio.Contact.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString("ContactDb")));

builder.Services.AddScoped<
    IContactRepository,
    ContactRepository>();

builder.Services.AddScoped<
    IContactService,
    ContactService>();

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