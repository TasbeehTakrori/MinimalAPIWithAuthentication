using MinimalAPIWithAuthentication.Authentication;
using MinimalAPIWithAuthentication.Repository;
using Asp.Versioning;
using Asp.Versioning.Conventions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddSingleton<IRepository, Repository>();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddJwtAuthorization();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("Api-Version");
});

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
                    .HasApiVersion(1.0)
                    .ReportApiVersions()
                    .Build();

app.MapPost("/login", LoginHandler.HandleLogin)
    .WithApiVersionSet(versionSet).IsApiVersionNeutral();

app.MapGet("/hello", () => "Hello World!")
    .WithApiVersionSet(versionSet).IsApiVersionNeutral(); ;

app.MapGet("/helloUser", () => "Hello User!")
    .RequireAuthorization()
    .WithApiVersionSet(versionSet).IsApiVersionNeutral(); ;

app.MapGet("/helloAdmin", () => "Hello Admin!")
    .RequireAuthorization("AdminPolicy")
    .WithApiVersionSet(versionSet).IsApiVersionNeutral(); ;

app.Run();