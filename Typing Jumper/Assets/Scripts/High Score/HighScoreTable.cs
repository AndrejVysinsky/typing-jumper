using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] Transform entryContainer;
    [SerializeField] Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;
    private GameObject closeParent;

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
        if (highScoreEntryTransformList != null)
        {
            foreach (var entry in highScoreEntryTransformList)
            {
                Destroy(entry.gameObject);
            }
            highScoreEntryTransformList.Clear();
        }

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        if (jsonString == null || jsonString == "")
        {
            // empty table
            return;
        }
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        highScoreEntryList = highScores.highScoreEntryList;

        highScoreEntryList = highScoreEntryList.OrderByDescending(e => e.score).ToList();

        highScoreEntryTransformList = new List<Transform>();
        foreach (var entry in highScoreEntryList.Take(10))
        {
            CreateHighScoreEntryTransform(entry, entryContainer, highScoreEntryTransformList);
        }
    }

    public void ShowLeaderboard(GameObject parent)
    {
        gameObject.SetActive(true);
        closeParent = parent;
        closeParent.SetActive(false);
    }

    public void ClearLeaderboard()
    {
        PlayerPrefs.SetString("highScoreTable", null);
        PlayerPrefs.Save();
        if(highScoreEntryTransformList != null)
        {
            foreach (var entry in highScoreEntryTransformList)
            {
                Destroy(entry.gameObject);
            }
            highScoreEntryTransformList.Clear();
        }
    }
    public void CloseLeaderboard()
    {
        gameObject.SetActive(false);
        if (closeParent != null)
        {
            closeParent.SetActive(true);
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 35f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform rectTransform = entryTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default:
                rankString = rank + "TH"; break;
        }

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        entryTransform.Find("posText").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;
        entryTransform.Find("scoreText").GetComponent<TMPro.TextMeshProUGUI>().text = highScoreEntry.score.ToString();
        entryTransform.Find("nameText").GetComponent<TMPro.TextMeshProUGUI>().text = highScoreEntry.name;

        switch (rank) { 
            case 1: entryTransform.Find("posText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.84f, 0f);
                    entryTransform.Find("scoreText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.84f, 0f);
                    entryTransform.Find("nameText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.84f, 0f);
                    break;
            case 2: entryTransform.Find("posText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.75f, 0.75f);
                    entryTransform.Find("scoreText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.75f, 0.75f);
                    entryTransform.Find("nameText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.75f, 0.75f);
                    break;
            case 3: entryTransform.Find("posText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.5f, 0.2f);
                    entryTransform.Find("scoreText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.5f, 0.2f);
                entryTransform.Find("nameText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.5f, 0.2f);
                break;

        }
        transformList.Add(entryTransform);
    }

    public void AddHighScoreEntry(int score, string name)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores;
        if (jsonString == null || jsonString == "")
        {
            highScores = new HighScores { highScoreEntryList = new List<HighScoreEntry>() };
        } else
        {
            highScores = JsonUtility.FromJson<HighScores>(jsonString);
        }
        
        highScores.highScoreEntryList.Add(highScoreEntry);

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
        Awake();
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }
}
