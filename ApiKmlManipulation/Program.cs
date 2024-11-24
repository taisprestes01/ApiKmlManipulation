using ApiKMLManipulation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy
            .AllowAnyOrigin()  
            .AllowAnyHeader()  
            .AllowAnyMethod();  
    });
});

builder.Services.AddScoped<KmlService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var relativePath = configuration["ApplicationSettings:KmlFilePath"];

    var filePath = Path.Combine(AppContext.BaseDirectory, relativePath);

    if (!File.Exists(filePath))
    {
        throw new FileNotFoundException($"Arquivo KML não encontrado: {filePath}");
    }

    return new KmlService(filePath);
});

builder.Services.AddScoped<PlacemarkService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.MapGet("/", () => "ApiKMLManipulation - API está funcionando!");

app.Run();

