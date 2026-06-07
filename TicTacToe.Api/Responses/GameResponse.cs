using TicTacToe.Api.Enums;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Responses;

public class GameResponse
{
    public Guid Id { get; set; }

    public string[][] Board { get; set; } = [];

    public Player CurrentPlayer { get; set; }

    public GameMode Mode { get; set; }

    public GameStatus Status { get; set; }

    public Player? Winner { get; set; }

    public List<int> WinningCells { get; set; } = new();

    public List<Move> MoveHistory { get; set; } = new();
}