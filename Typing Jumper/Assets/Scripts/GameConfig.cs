using UnityEngine;

public class GameConfig : MonoBehaviour
{
    private readonly string _difficultyKey = "Difficulty";

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
}
