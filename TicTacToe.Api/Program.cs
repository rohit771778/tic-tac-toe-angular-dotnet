using TicTacToe.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddSingleton<IScoreboardService, ScoreboardService>();
builder.Services.AddSingleton<ComputerMoveService>();
builder.Services.AddSingleton<IGameService, GameService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection();

app.UseCors("Angular");

app.MapControllers();

app.Run();