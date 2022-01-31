using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SimpleJSON;

/// <summary>
/// For new high score entry.
/// </summary>
public class APIWrapper
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string BASE_API_URL = "http://127.0.0.1:8000/api/";
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

        return response.IsSuccessStatusCode;
    }

    public async Task<List<ResultModel>> getLeaderboard(int difficulty, int gameMode)
    {
        string url = LEADERBOARD_API_URL + $"?game_mode={gameMode}&difficulty={difficulty}";
        var response = await client.GetAsync(url);
        var responseString = await response.Content.ReadAsStringAsync();

        JSONNode itemsData = JSON.Parse(responseString);

        List<ResultModel> resultModels = new List<ResultModel>();

        for (int i = 0; i < itemsData["data"].Count; i++)
        {
            var item = itemsData["data"][i];
            var resultModel = new ResultModel()
            {
                id = item["id"],
                username = item["username"],
                score = item["score"],
                game_mode = item["game_mode"],
                difficulty = item["difficulty"]
            };
            resultModels.Add(resultModel);
        }

        return resultModels;
    }
}