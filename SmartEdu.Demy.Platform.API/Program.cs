using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartEdu.Demy.Platform.API.Billing.Application.Internal.QueryServices;
using SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Billing.Domain.Services;
using SmartEdu.Demy.Platform.API.Billing.Infrastructure.Persistence.EFC.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.CommandServices;
using SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.QueryServices;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Scheduling.Infrastructure.Persistence.EFC.Repositories;
using SmartEdu.Demy.Platform.API.Attendance.Application.Internal.CommandServices;
using SmartEdu.Demy.Platform.API.Attendance.Application.Internal.QueryServices;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
using SmartEdu.Demy.Platform.API.Attendance.Infrastructure.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.CommandServices;
using SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.OutboundServices.ACL;
using SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;
using SmartEdu.Demy.Platform.API.Enrollment.Infrastructure.Persistence.EFC.Repositories;
using SmartEdu.Demy.Platform.API.Iam.Application.Internal.CommandServices;
using SmartEdu.Demy.Platform.API.Iam.Application.Internal.OutboundServices;
using SmartEdu.Demy.Platform.API.Iam.Application.Internal.QueryServices;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.EFC;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Hashing.BCrypt.Services;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Tokens.JWT.Configuration;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Tokens.JWT.Services;
using SmartEdu.Demy.Platform.API.Scheduling.Application.ACL;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.ACL;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Pipeline.Middleware.Components;

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
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// Dependency Injection Configuration

// Shared Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Billing Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceQueryService, InvoiceQueryService>();

// Attendance Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IClassSessionRepository, ClassSessionRepository>();
builder.Services.AddScoped<IClassSessionCommandService, ClassSessionCommandService>();
builder.Services.AddScoped<IClassSessionQueryService, ClassSessionQueryService>();

// Scheduling Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<ICourseCommandService, CourseCommandService>();
builder.Services.AddScoped<ICourseQueryService, CourseQueryService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<IClassroomCommandService, ClassroomCommandService>();
builder.Services.AddScoped<IClassroomQueryService, ClassroomQueryService>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();

builder.Services.AddScoped<IWeeklyScheduleCommandService, WeeklyScheduleCommandService>();
builder.Services.AddScoped<IWeeklyScheduleQueryService, WeeklyScheduleQueryService>();
builder.Services.AddScoped<IWeeklyScheduleRepository, WeeklyScheduleRepository>();
builder.Services.AddScoped<ISchedulingsContextFacade, SchedulingsContextFacade>();


builder.Services.AddScoped<IScheduleQueryService, ScheduleQueryService>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

// Enrollment Bounded Context Dependency Injection Configuration
// AcademicPeriod
builder.Services.AddScoped<IAcademicPeriodCommandService, AcademicPeriodCommandService>();
builder.Services.AddScoped<IAcademicPeriodQueryService, AcademicPeriodQueryService>();
builder.Services.AddScoped<IAcademicPeriodRepository, AcademicPeriodRepository>();

// Enrollment
builder.Services.AddScoped<IEnrollmentCommandService, EnrollmentCommandService>();
builder.Services.AddScoped<IEnrollmentQueryService, EnrollmentQueryService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<ExternalSchedulingService>();

// Student
builder.Services.AddScoped<IStudentCommandService, StudentCommandService>();
builder.Services.AddScoped<IStudentQueryService, StudentQueryService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Iam Bounded Context

// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// Dependency Injection for IAM Bounded Context
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserAccountRepository, UserRepository>();
builder.Services.AddScoped<IUserAccountQueryService, UserAccountQueryService>();
builder.Services.AddScoped<IUserAccountCommandService, UserAccountCommandService>();

// Add this to bind to Railway's assigned PORT
if (builder.Environment.IsProduction())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
    builder.WebHost.UseUrls($"http://*:{port}");
}

var app = builder.Build();

// Verify Database Objects are Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Global error handling middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Enable CORS
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

// HTTPS Redirection
app.UseHttpsRedirection();

// Authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();