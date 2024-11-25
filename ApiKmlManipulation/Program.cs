using ApiKMLManipulation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy
            .AllowAnyOrigin()  // Adjust in production to restrict origins
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services
builder.Services.AddScoped<KmlService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var relativePath = configuration["ApplicationSettings:KmlFilePath"];

    if (string.IsNullOrWhiteSpace(relativePath))
    {
        throw new InvalidOperationException("ApplicationSettings:KmlFilePath is not configured in appsettings.json.");
    }

    var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
    if (projectDirectory == null)
    {
        throw new InvalidOperationException("Project directory structure is invalid. Unable to determine root directory.");
    }

    var filePath = Path.Combine(projectDirectory, relativePath);

    if (!File.Exists(filePath))
    {
        throw new FileNotFoundException($"KML file not found at the specified path: {filePath}");
    }

    if (new FileInfo(filePath).Length == 0)
    {
        throw new InvalidDataException("The KML file is empty. Please provide a valid KML file.");
    }

    return new KmlService(filePath);
});

builder.Services.AddScoped<PlacemarkService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Optionally disable Swagger in production
    // app.UseSwagger(); 
    // app.UseSwaggerUI();
}

// Redirect to HTTPS
app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAnyOrigin");

// Map endpoints
app.MapControllers();

// Health check endpoint
app.MapGet("/", () => "ApiKMLManipulation - API está funcionando!");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Unhandled exception: {ex.Message}");
    throw;
}
