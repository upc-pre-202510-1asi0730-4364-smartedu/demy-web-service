using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Configuration for Routing
builder.Services.AddControllers();

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
    throw new Exception("Connection string is null");

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
});

// OpenAPI/Swagger Configuration
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SmartEdu Demy Platform API",
        Version = "v1",
        Description = "Backend RESTful API for SmartEdu Demy"
    });
    // options.EnableAnnotations();
});

// Add your application services, for example:
// builder.Services.AddScoped<IUserQueryService, UserQueryService>();

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

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();