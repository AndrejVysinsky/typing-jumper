using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// For new high score entry.
/// </summary>
public class RequestModel
{
    public RequestModel(string username, int score, int gameMode, string gameDifficulty)
    {
        Username = username;
        Score = score;
        GameMode = gameMode;
        GameDifficulty = gameDifficulty;
    }
    public string Username { get; set; }
    public int Score { get; set; }
    public int GameMode { get; set; }
    public string GameDifficulty { get; set; }
}