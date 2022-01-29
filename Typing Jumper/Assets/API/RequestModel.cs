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
    public string Name { get; set; }
    public int Score { get; set; }
    public int GameMode { get; set; }
    public int GameDifficulty { get; set; }
}