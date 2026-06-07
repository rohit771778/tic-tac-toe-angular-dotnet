using TicTacToe.Api.Models;

namespace TicTacToe.Api.Services;

public class ScoreboardService : IScoreboardService
{
    private readonly Scoreboard _scoreboard = new();

    public Scoreboard GetScoreboard()
    {
        return _scoreboard;
    }

    public void IncrementXWin()
    {
        _scoreboard.XWins++;
    }

    public void IncrementOWin()
    {
        _scoreboard.OWins++;
    }

    public void IncrementDraw()
    {
        _scoreboard.Draws++;
    }

    public void Reset()
    {
        _scoreboard.XWins = 0;
        _scoreboard.OWins = 0;
        _scoreboard.Draws = 0;
    }
}