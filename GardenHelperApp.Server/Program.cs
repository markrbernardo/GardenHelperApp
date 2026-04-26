using GardenHelperApp.Server.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ⭐ Force ASP.NET Core to treat all incoming timestamps as LOCAL time
AppContext.SetSwitch("System.Globalization.EnforceLocalTime", true);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GardenContext>(options =>
    options.UseSqlite("Data Source=Data/GardenHelper.db"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GardenContext>();
    db.Database.EnsureCreated();
}

// ⭐ CORS MUST BE FIRST
app.UseCors();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
