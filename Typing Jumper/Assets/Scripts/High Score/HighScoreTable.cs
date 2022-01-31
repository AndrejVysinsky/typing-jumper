using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] GameObject entryContainer;
    [SerializeField] GameObject highScoreEntryPrefab;
    private List<GameObject> entryObjects;

    [SerializeField] GameObject casualContainer;
    [SerializeField] GameObject competitiveContainer;

    [SerializeField] HighScoreButton casualButton;
    [SerializeField] HighScoreButton competitiveButton;

    [SerializeField] HighScoreButton[] casualModeButtons;
    [SerializeField] HighScoreButton[] competitiveModeButtons;

    private int _gameModeIndex = 0;
    private int _casualModeIndex = 0;
    private int _competitiveModeIndex = 0;

    private void Awake()
    {
        ShowResults();
    }

    private void OnEnable()
    {
        ShowResults();
    }

    private void ShowResults()
    {
        if (_gameModeIndex == (int)GameModeEnum.Casual)
        {
            ActivateCasual();
        }
        else
        {
            ActivateCompetitive();
        }
    }

    public void ActivateCasual()
    {
        _gameModeIndex = 0;

        casualButton.ActivateButton();
        competitiveButton.DeactivateButton();

        casualContainer.SetActive(true);
        competitiveContainer.SetActive(false);

        LoadCasualEntries(_casualModeIndex);
    }

    public void ActivateCompetitive()
    {
        _gameModeIndex = 1;

        casualButton.DeactivateButton();
        competitiveButton.ActivateButton();

        casualContainer.SetActive(false);
        competitiveContainer.SetActive(true);

        LoadCompetitiveEntries(_competitiveModeIndex);
    }

    public void LoadCasualEntries(int difficultyIndex)
    {
        _casualModeIndex = difficultyIndex;

        for (int i = 0; i < casualModeButtons.Length; i++)
        {
            casualModeButtons[i].DeactivateButton();
        }
        casualModeButtons[_casualModeIndex].ActivateButton();

        var wrapper = new APIWrapper();
        var result = Task.Run(() => wrapper.getLeaderboard(difficultyIndex, (int)GameModeEnum.Casual)).Result;
        
        var results = result.ToArray();
        results = results.Where(x => x.difficulty == difficultyIndex).ToArray();

        ShowHighScores(results);
    }

    public void LoadCompetitiveEntries(int timeLimitIndex)
    {
        _competitiveModeIndex = timeLimitIndex;

        for (int i = 0; i < competitiveModeButtons.Length; i++)
        {
            competitiveModeButtons[i].DeactivateButton();
        }
        competitiveModeButtons[_competitiveModeIndex].ActivateButton();


        var wrapper = new APIWrapper();
        var result = Task.Run(() => wrapper.getLeaderboard(timeLimitIndex, (int)GameModeEnum.Competitive)).Result;

        var results = result.ToArray();
        results = results.Where(x => x.difficulty == timeLimitIndex).ToArray();

        ShowHighScores(results);
    }

    private void ShowHighScores(ResultModel[] results)
    {
        if (entryObjects == null)
            entryObjects = new List<GameObject>();

        for (int i = 0; i < entryObjects.Count; i++)
        {
            Destroy(entryObjects[i]);
        }
        entryObjects.Clear();

        results = results.OrderByDescending(x => x.score).ToArray();

        int max = results.Length > 10 ? 10 : results.Length;
        for (int i = 0; i < max; i++)
        {
            var entryObject = Instantiate(highScoreEntryPrefab, entryContainer.transform);
            entryObjects.Add(entryObject);

            InitializeHighScoreEntry(i + 1, entryObject, results[i]);
        }
    }

    public void CloseLeaderboard()
    {
        gameObject.SetActive(false);
    }

    private void InitializeHighScoreEntry(int rank, GameObject entryObject, ResultModel resultModel)
    {     
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default:
                rankString = rank + "TH"; break;
        }

        //entryObject.GetComponent<Image>().enabled = rank % 2 == 1;

        var positionText = entryObject.transform.Find("Position Text").GetComponent<TMPro.TextMeshProUGUI>();
        var scoreText = entryObject.transform.Find("Score Text").GetComponent<TMPro.TextMeshProUGUI>();
        var nameText = entryObject.transform.Find("Name Text").GetComponent<TMPro.TextMeshProUGUI>();

        positionText.text = rankString;
        scoreText.text = resultModel.score.ToString();
        nameText.text = resultModel.username;

        switch (rank) { 
            case 1:
                positionText.color = new Color(1f, 0.84f, 0f);
                scoreText.color = new Color(1f, 0.84f, 0f);
                nameText.color = new Color(1f, 0.84f, 0f);
                break;
            case 2:
                positionText.color = new Color(0.75f, 0.75f, 0.75f);
                scoreText.color = new Color(0.75f, 0.75f, 0.75f);
                nameText.color = new Color(0.75f, 0.75f, 0.75f);
                break;
            case 3:
                positionText.color = new Color(0.8f, 0.5f, 0.2f);
                scoreText.color = new Color(0.8f, 0.5f, 0.2f);
                nameText.color = new Color(0.8f, 0.5f, 0.2f);
                break;
        }
    }

    public bool AddHighScoreEntry(int score, string name)
    {
        var gameMode = (int)GameConfig.Instance.GetGameMode();
        var gameDifficulty = 0;

        if (gameMode == (int)GameModeEnum.Casual)
        {
            gameDifficulty = (int)GameConfig.Instance.GetDifficulty();
        }
        else if (gameMode == (int)GameModeEnum.Competitive)
        {
            gameDifficulty = (int)GameConfig.Instance.GetTimeLimit();
        }
        else
        {
            return false;
        }

        RequestModel request = new RequestModel()
        {
            username = name,
            score = score,
            game_mode = gameMode,
            difficulty = gameDifficulty
        };

        var wrapper = new APIWrapper();
        var result = Task.Run(() => wrapper.submitScore(request)).Result;
        return result;
    }
}
