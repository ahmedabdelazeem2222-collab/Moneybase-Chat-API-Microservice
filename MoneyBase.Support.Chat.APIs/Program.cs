using Microsoft.EntityFrameworkCore;
using MoneyBase.Support.Infrastructure.Persistence;
using MoneyBase.Support.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DB Connection String
builder.Services.AddDbContext<MoneyBaseContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Configure Serilog
string logPath = builder.Configuration["Logging:Url:LogPath"];
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
#endregion

#region IOC container - register services
builder.Services.AddMoneyBaseServices(builder.Configuration)
                .AddHostedServices();
#endregion

var app = builder.Build();

app.MapGet("/health", () => "Hello MoneyBase.Support.Chat.APIs");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MoneyBaseContext>();
    //db.Database.Migrate();
    //await DbInitializer.SeedAsync(db);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Starting MoneyBase.Support.Chat.APIs application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed!");
}
finally
{
    Log.CloseAndFlush();
}
