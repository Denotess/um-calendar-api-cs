using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.FileSystemGlobbing;

var builder = WebApplication.CreateBuilder();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "calendar-api";
    config.Title = "Calendar Api";
    config.Version = "v1";
});

var app = builder.Build();
app.UseOpenApi();
app.UseSwaggerUi();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});


var rootPath = Directory.GetCurrentDirectory();
var calendarPath = Path.Combine(rootPath, "calendars");
Directory.CreateDirectory(calendarPath);
string[] icsFiles = Directory.GetFiles(calendarPath, "*.ics");



app.MapGet("/health", () =>
{
    return new { status = "healthy" };
});

app.MapGet("/names.json", () =>
{
    var names = icsFiles
    .Select(f => Path.GetFileNameWithoutExtension(f))
    .OrderBy(n => n)
    .ToArray();
    
    return names;
});

app.MapGet("/cal/{name}", (string name) =>
{
    var filePath = Path.Combine(calendarPath, name + ".ics");
    if (!File.Exists(filePath))
    {
        return Results.NotFound();
    }
    var fileContent = File.ReadAllText(filePath);
    return Results.Content(fileContent, "text/calendar; charset=utf-8");
});

app.Run();
