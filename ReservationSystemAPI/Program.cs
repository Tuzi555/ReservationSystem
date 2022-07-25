using DataAccess.DbAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReservationSystemAPI.Auth;
using Services.Logic;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\"). For demo purposes tokens have no expiration. Below are provided auth tokens for admin and user. <br/><br/> admin: 'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtZUBqYWt1YnR1emFyLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiZXhwIjoxNjkwMDM5OTUzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjQwIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI0MCJ9.75KIYRiPrHIXVRf6Y2DyzyLPScgH4KRwDk8RDCtJtscfxvAYFA583mIZbhXnZe2zPO7cDtj8IwNZgx5uuQPztQ'<br/><br/> user: 'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtZUBoYW5rYS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJ1c2VyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIyIiwiZXhwIjoxNjkwMDQwMDIxLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjQwIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI0MCJ9.4sz5Vmm58oD-acAIkCIyzHNnFqtTU7DknP4tbwlY7hJSCNkaS5r2Syj4y7CwRUA0KpgzCMDF1FJiKLZWfnyGSQ'",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddTransient<IClassScheduleData, ClassScheduleData>();
builder.Services.AddSingleton<IClassData, ClassData>();
builder.Services.AddSingleton<IReservationData, ReservationData>();
builder.Services.AddSingleton<IAuthTokenCreator, AuthTokenCreator>();
builder.Services.AddSingleton<IUserIdentifier, UserIdentifier>();
builder.Services.AddSingleton<IReservationCreationValidator, ReservationCreationValidator>();
builder.Services.AddSingleton<IClassScheduleCreationValidator, ClassScheduleCreationValidator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration.GetSection("AppSettings:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("AppSettings:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value))
    };
    options.SaveToken = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Yoga Class Reservation API (Work in Progress)",
        Description = "This API is being built to enable small yoga business to move from \"pen and paper\" reservation system to online reservation system. This demo is being hosted on Microsoft Azure and uses Azure SQL Database to store data. The API itself is being built using .NET 6. Azure Automation Job runs every night at midnight CET/CEST to restore database to default demo state.",
        Contact = new OpenApiContact
        {
            Name = "Jakub Tuzar",
            Email = "jakub.tuzar@gmail.com",
            Url = new Uri("https://www.jakubtuzar.com")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        },
        Version = "v1"
    });
    //generate xml docs that will drive the swagger docs
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
