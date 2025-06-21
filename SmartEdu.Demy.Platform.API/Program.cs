using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.CommandServices;
using SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.QueryServices;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Scheduling.Infrastructure.Persistence.EFC.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Configuration for Routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString is null)
    throw new InvalidOperationException("Connection string is null");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
    }
});

// Configure Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SmartEdu Demy Platform API",
        Version = "v1",
        Description = "Backend RESTful API for SmartEdu Demy"
    });
});

// Register Unit of Work and other shared services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add the application services, for example:
// builder.Services.AddScoped<IUserQueryService, UserQueryService>();

builder.Services.AddScoped<ICourseCommandService, CourseCommandService>();
builder.Services.AddScoped<ICourseQueryService, CourseQueryService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<IClassroomCommandService, ClassroomCommandService>();
builder.Services.AddScoped<IClassroomQueryService, ClassroomQueryService>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();

builder.Services.AddScoped<IWeeklyScheduleCommandService, WeeklyScheduleCommandService>();
builder.Services.AddScoped<IWeeklyScheduleQueryService, WeeklyScheduleQueryService>();
builder.Services.AddScoped<IWeeklyScheduleRepository, WeeklyScheduleRepository>();



var app = builder.Build();

// Verify Database Objects are Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS
app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();