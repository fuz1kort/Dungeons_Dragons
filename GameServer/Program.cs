using GameServer.Data;
using GameServer.Services.LogicService;
using GameServer.Services.MonsterService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ILogicService, LogicService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("database")));
builder.Services.AddScoped<IRandomMonsterService, RandomMonsterService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();