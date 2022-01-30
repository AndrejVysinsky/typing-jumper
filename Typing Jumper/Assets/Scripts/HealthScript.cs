using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] GameObject healthContainer;
    [SerializeField] List<Image> healthPoints;

    [SerializeField] GameObject timeContainer;
    [SerializeField] TextMeshProUGUI timeText;

    private GameModeEnum _activeGameMode;

    private float _remainingTime;

    private void Start()
    {
        _activeGameMode = GameConfig.Instance.GetGameMode();
        if (_activeGameMode == GameModeEnum.Casual)
        {
            var difficulty = GameConfig.Instance.GetDifficulty();

            switch (difficulty)
            {
                case DifficultyEnum.Easy:
                    InitializeHealth(5);
                    break;
                case DifficultyEnum.Medium:
                    InitializeHealth(3);
                    break;
                case DifficultyEnum.Hard:
                    InitializeHealth(1);
                    break;
            }
        }
        else if (_activeGameMode == GameModeEnum.Competitive)
        {
            var timeLimit = GameConfig.Instance.GetTimeLimit();

            switch (timeLimit)
            {
                case TimeLimitEnum.Seconds30:
                    InitializeTime(30);
                    break;
                case TimeLimitEnum.Seconds60:
                    InitializeTime(60);
                    break;
            }
        }
        else
        {
            FindObjectOfType<SceneLoader>().LoadScene(0);
        }
    }

    

    private void Update()
    {
        if (_activeGameMode == GameModeEnum.Competitive)
        {
            if (_remainingTime <= 0)
                return;

            _remainingTime -= Time.deltaTime;

            if (_remainingTime < 0)
            {
                _remainingTime = 0;
            }

            int seconds = Mathf.RoundToInt(_remainingTime);
            timeText.text = seconds.ToString();
        }
    }

    public bool IsOutOfTime()
    {
        if (_activeGameMode == GameModeEnum.Competitive)
        {
            return _remainingTime <= 0;
        }
        return false;
    }

    private void InitializeTime(int numberOfSeconds)
    {
        healthContainer.SetActive(false);
        timeContainer.SetActive(true);

        _remainingTime = numberOfSeconds;
    }

    private void InitializeHealth(int numberOfHealthPoints)
    {
        healthContainer.SetActive(true);
        timeContainer.SetActive(false);

        for (int i = 0; i < healthPoints.Count; i++)
        {
            if (i >= numberOfHealthPoints)
            {
                healthPoints[i].gameObject.SetActive(false);
            }
            else
            {
                healthPoints[i].gameObject.SetActive(true);
            }
        }
    }

    public int RemoveHealthPoint()
    {
        int remainingHealthPoints = 0;
        for (int i = healthPoints.Count - 1; i >= 0; i--)
        {
            if (healthPoints[i].gameObject.activeSelf)
            {
                healthPoints[i].gameObject.SetActive(false);
                remainingHealthPoints = i;
                break;
            }
        }

        return remainingHealthPoints;
    }
}