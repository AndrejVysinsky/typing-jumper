using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// For new high score entry.
/// </summary>
[Serializable]
public class RequestModel
{
    [SerializeField] public string username;
    [SerializeField] public int score;
    [SerializeField] public int game_mode;
    [SerializeField] public int difficulty;
}