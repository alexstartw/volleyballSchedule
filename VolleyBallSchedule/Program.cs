using System.Net;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using VolleyBallSchedule;
using VolleyBallSchedule.Repos;
using VolleyBallSchedule.Repos.Interfaces;
using VolleyBallSchedule.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IPlayerRepo, PlayerRepo>();
builder.Services.AddScoped<ISeasonPlayerRepo, SeasonPlayerRepo>();
builder.Services.AddScoped<ILineBotService, LineBotService>();
builder.Services.AddDbContext<PlayerContext>(options =>
{
    options.UseNpgsql("Host=192.168.50.50;Port=5432;Database=postgres;Username=postgres;Password=12345678;");
});

// 配置 Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)  // 排除 Microsoft 系統日誌
    .MinimumLevel.Override("System", LogEventLevel.Warning)     // 排除 System 系統日誌
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")  // 這裡的 URL 應替換為你的 Seq 服務器地址
    .CreateLogger();

builder.Host.UseSerilog();  // 使用 Serilog 作為日誌提供者


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();