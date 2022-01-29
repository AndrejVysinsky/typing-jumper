using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] GameObject entryContainer;
    [SerializeField] GameObject highScoreEntryPrefab;
    private List<HighScoreEntry> entryData;
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
        ActivateDefaultButtons();
        LoadHighScoreEntries();
    }

    private void OnEnable()
    {
        ActivateDefaultButtons();
        LoadHighScoreEntries();
    }

    private void ActivateDefaultButtons()
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

        //get json from api
        string[] result = new string[3];

        ShowHighScores(result);
    }

    public void LoadCompetitiveEntries(int timeLimitIndex)
    {
        _competitiveModeIndex = timeLimitIndex;

        for (int i = 0; i < competitiveModeButtons.Length; i++)
        {
            competitiveModeButtons[i].DeactivateButton();
        }
        competitiveModeButtons[_competitiveModeIndex].ActivateButton();

        //get json from api
        string[] result = new string[3];

        ShowHighScores(result);
    }

    private void ShowHighScores(string[] apiResult)
    {

    }

    private void LoadHighScoreEntries()
    {
        if (entryObjects == null)
            entryObjects = new List<GameObject>();

        for (int i = 0; i < entryObjects.Count; i++)
        {
            Destroy(entryObjects[i]);
        }
        entryObjects.Clear();

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        if (jsonString == null || jsonString == "")
        {
            // empty table
            return;
        }
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        entryData = highScores.entryData;

        entryData = entryData.OrderByDescending(e => e.score).ToList();

        int max = entryData.Count > 10 ? 10 : entryData.Count;
        for (int i = 0; i < max; i++)
        {
            var entryObject = Instantiate(highScoreEntryPrefab, entryContainer.transform);
            entryObjects.Add(entryObject);

            InitializeHighScoreEntry(i + 1, entryObject, entryData[i]);
        }
    }

    public void ShowLeaderboard()
    {
        gameObject.SetActive(true);
        LoadHighScoreEntries();
    }

    public void ClearLeaderboard()
    {
        PlayerPrefs.SetString("highScoreTable", null);
        PlayerPrefs.Save();
    }
    public void CloseLeaderboard()
    {
        gameObject.SetActive(false);
    }

    private void InitializeHighScoreEntry(int rank, GameObject entryObject, HighScoreEntry highScoreEntry)
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
        scoreText.text = highScoreEntry.score.ToString();
        nameText.text = highScoreEntry.name;

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

    public void AddHighScoreEntry(int score, string name)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores;
        if (jsonString == null || jsonString == "")
        {
            highScores = new HighScores { entryData = new List<HighScoreEntry>() };
        } else
        {
            highScores = JsonUtility.FromJson<HighScores>(jsonString);
        }
        
        highScores.entryData.Add(highScoreEntry);

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
        LoadHighScoreEntries();
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }

    private class HighScores
    {
        public List<HighScoreEntry> entryData;
    }
}
