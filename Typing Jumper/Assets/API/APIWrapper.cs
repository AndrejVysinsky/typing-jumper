using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Assets.API.DTO;
using UnityEngine;

/// <summary>
/// For new high score entry.
/// </summary>
public class APIWrapper
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string BASE_API_URL = "http://127.0.0.1:8000/";
    private static readonly string SCORE_ENTRY_API_URL = BASE_API_URL + "score-entry/";
    private static readonly string PLAYER_API_URL = BASE_API_URL + "player/";
    private static readonly string LEADERBOARD_API_URL = BASE_API_URL + "leaderboard/";


    public async Task<bool> submitScore(RequestModel scoreEntry)
    {
        string scoreJson = JsonUtility.ToJson(scoreEntry);

        // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
        var httpContent = new StringContent(scoreJson, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(SCORE_ENTRY_API_URL, httpContent);

        var responseString = await response.Content.ReadAsStringAsync();

        return true;
    }
}