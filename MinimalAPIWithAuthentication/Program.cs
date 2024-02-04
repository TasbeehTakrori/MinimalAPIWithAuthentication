using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinimalAPIWithAuthentication.Authentication;
using MinimalAPIWithAuthentication.DTOs;
using MinimalAPIWithAuthentication.Entities;
using MinimalAPIWithAuthentication.Enums;
using MinimalAPIWithAuthentication.Repository;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(ClaimTypes.Role, "Admin");
    });
});



builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IRepository, Repository>();

var app = builder.Build();

app.MapPost("/login", ([FromBody] AuthenticationRequestBody authenticationRequestBody, ITokenGenerator tokenGenerator, IRepository repository) =>
{
    var user = repository.Find(authenticationRequestBody.UserName, authenticationRequestBody.Password);
    if (user is null)
        return Results.NotFound(new { message = "Invalid username or password" });

    var token = tokenGenerator.GenerateToken(user);
    var userResponse = new UserResponseDTO
    {
        Token = token,
        Name = user.Name,
        Role = Enum.GetName(typeof(Role), user.Role)
    };
    return Results.Ok(userResponse);
});

app.MapGet("/hello", () => "Hello World!");

app.MapGet("/helloUser", () => "Hello User!")
    .RequireAuthorization();

app.MapGet("/helloAdmin", () => "Hello Admin!")
    .RequireAuthorization("AdminPolicy");

app.Run();