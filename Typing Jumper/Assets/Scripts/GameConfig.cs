using UnityEngine;

public class GameConfig : MonoBehaviour
{
    private readonly string _gameModeKey = "GameMode";
    private readonly string _difficultyKey = "Difficulty";
    private readonly string _timeLimitKey = "TimeLimit";

    public static GameConfig Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameMode(int gameMode)
    {
        PlayerPrefs.SetInt(_gameModeKey, gameMode);
    }

    public GameModeEnum GetGameMode()
    {
        if (PlayerPrefs.HasKey(_gameModeKey))
        {
            return (GameModeEnum)PlayerPrefs.GetInt(_gameModeKey);
        }

        return GameModeEnum.Casual;
    }

    public void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(_difficultyKey, difficulty);
    }

    public DifficultyEnum GetDifficulty()
    {
        if (PlayerPrefs.HasKey(_difficultyKey))
        {
            return (DifficultyEnum)PlayerPrefs.GetInt(_difficultyKey);
        }

        return DifficultyEnum.Easy;
    }

    public void SetTimeLimit(int timeLimit)
    {
        PlayerPrefs.SetInt(_timeLimitKey, timeLimit);
    }

    public TimeLimitEnum GetTimeLimit()
    {
        if (PlayerPrefs.HasKey(_timeLimitKey))
        {
            return (TimeLimitEnum)PlayerPrefs.GetInt(_timeLimitKey);
        }

        return TimeLimitEnum.Seconds60;
    }
}
