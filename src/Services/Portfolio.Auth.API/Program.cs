using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Auth.API.Interfaces;
using Portfolio.Auth.API.Models;
using Portfolio.Auth.API.Services;
using Portfolio.Shared.Infrastructure.Interfaces;
using Portfolio.Shared.Infrastructure.Models;
using Portfolio.Shared.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();


builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AuthDb")
    ));

builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(
                "JwtSettings"));

// JWT Authentication
var jwt = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwt["Issuer"],
                ValidAudience = jwt["Audience"],

                IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["JwtSettings:SecretKey"]))
            };
    });
builder.Services.AddAuthorization();
// Services
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthentication();   // MUST come before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
