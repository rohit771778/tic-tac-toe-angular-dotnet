using TicTacToe.Api.Enums;
using TicTacToe.Api.Models;
using TicTacToe.Api.Requests;

namespace TicTacToe.Api.Services;

public class GameService : IGameService
{
    private readonly Dictionary<Guid, GameState> _games = new();

    private readonly IScoreboardService _scoreboard;

    private readonly ComputerMoveService _computerMoveService;

    public GameService(
        IScoreboardService scoreboard,
        ComputerMoveService computerMoveService)
    {
        _scoreboard = scoreboard;
        _computerMoveService = computerMoveService;
    }

    public GameState CreateGame(GameMode mode)
    {
        var game = new GameState
        {
            Id = Guid.NewGuid(),
            Board =
            [
                ["", "", ""],
                ["", "", ""],
                ["", "", ""]
            ],
            CurrentPlayer = Player.X,
            Mode = mode,
            Status = GameStatus.InProgress
        };

        _games.Add(game.Id, game);

        return game;
    }

    public GameState GetGame(Guid gameId)
    {
        return _games[gameId];
    }

public GameState MakeMove(Guid gameId, MoveRequest request)
{
    var game = _games[gameId];

    if (game.Status != GameStatus.InProgress)
        throw new Exception("Game already completed.");

    if (request.Row < 0 || request.Row > 2 ||
        request.Column < 0 || request.Column > 2)
        throw new Exception("Invalid position.");

    if (!string.IsNullOrEmpty(game.Board[request.Row][request.Column]))
        throw new Exception("Cell already occupied.");

    if (game.CurrentPlayer != request.Player)
        throw new Exception("Wrong player turn.");

    game.Board[request.Row][request.Column] = request.Player.ToString();

    game.MoveHistory.Add(new Move
    {
        MoveNumber = game.MoveHistory.Count + 1,
        Player = request.Player,
        Row = request.Row,
        Column = request.Column
    });

        CheckWinner(game);
if (game.Mode == GameMode.Computer
    && game.Status == GameStatus.InProgress
    && request.Player == Player.X)
{
    game.CurrentPlayer = Player.O;
    MakeComputerMove(game);
}
else if (game.Status == GameStatus.InProgress)
{
    game.CurrentPlayer =
        game.CurrentPlayer == Player.X
        ? Player.O
        : Player.X;
}

    return game;
}
private void CheckWinner(GameState game)
{
    var board = game.Board;

    int[][] patterns =
    {
        new[] {0,1,2},
        new[] {3,4,5},
        new[] {6,7,8},

        new[] {0,3,6},
        new[] {1,4,7},
        new[] {2,5,8},

        new[] {0,4,8},
        new[] {2,4,6}
    };

    foreach (var pattern in patterns)
    {
        string a = GetCell(board, pattern[0]);
        string b = GetCell(board, pattern[1]);
        string c = GetCell(board, pattern[2]);

        if (!string.IsNullOrEmpty(a)
            && a == b
            && b == c)
        {
            game.Status = GameStatus.Won;

            game.Winner =
                a == "X"
                ? Player.X
                : Player.O;

            game.WinningCells = pattern.ToList();

            if (game.Winner == Player.X)
                _scoreboard.IncrementXWin();
            else
                _scoreboard.IncrementOWin();

            return;
        }
    }

    bool draw = true;

    for (int r = 0; r < 3; r++)
    {
        for (int c = 0; c < 3; c++)
        {
            if (string.IsNullOrEmpty(board[r][c]))
            {
                draw = false;
                break;
            }
        }
    }

    if (draw)
    {
        game.Status = GameStatus.Draw;
        _scoreboard.IncrementDraw();
    }
}

private string GetCell(string[][] board, int index)
{
    int row = index / 3;
    int col = index % 3;

    return board[row][col];
}

public GameState Undo(Guid gameId)
{
    var game = _games[gameId];

    if (!game.MoveHistory.Any())
        return game;

    game.Status = GameStatus.InProgress;
    game.Winner = null;
    game.WinningCells.Clear();

    if (game.Mode == GameMode.Computer)
    {
        int count = Math.Min(2, game.MoveHistory.Count);

        for (int i = 0; i < count; i++)
        {
            var move = game.MoveHistory.Last();

            game.Board[move.Row][move.Column] = "";

            game.MoveHistory.RemoveAt(game.MoveHistory.Count - 1);
        }

        game.CurrentPlayer = Player.X;
    }
    else
    {
        var move = game.MoveHistory.Last();

        game.Board[move.Row][move.Column] = "";

        game.MoveHistory.RemoveAt(game.MoveHistory.Count - 1);

        game.CurrentPlayer = move.Player;
    }

    return game;
}

public GameState Reset(Guid gameId)
{
    var game = _games[gameId];

    game.Board = new string[][]
    {
        new string[] {"","",""},
        new string[] {"","",""},
        new string[] {"","",""}
    };

    game.CurrentPlayer = Player.X;

    game.Status = GameStatus.InProgress;

    game.Winner = null;

    game.WinningCells.Clear();

    game.MoveHistory.Clear();

    return game;
}
private void MakeComputerMove(GameState game)
{
    var move = _computerMoveService.GetMove(game.Board);

    if (move == null)
        return;

    game.Board[move.Value.row][move.Value.col] = "O";

    game.MoveHistory.Add(new Move
    {
        MoveNumber = game.MoveHistory.Count + 1,
        Player = Player.O,
        Row = move.Value.row,
        Column = move.Value.col
    });

    CheckWinner(game);

    if (game.Status == GameStatus.InProgress)
    {
        game.CurrentPlayer = Player.X;
    }
}
}