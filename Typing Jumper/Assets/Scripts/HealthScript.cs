using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] List<Image> healthPoints;

    private void Start()
    {
        var difficulty = GameConfig.Instance.GetDifficulty();

        switch (difficulty)
        {
            case DifficultyEnum.Easy:
                Initialize(7);
                break;
            case DifficultyEnum.Medium:
                Initialize(5);
                break;
            case DifficultyEnum.Hard:
                Initialize(3);
                break;
        }
    }

    private void Initialize(int numberOfHealthPoints)
    {
        Debug.Log(numberOfHealthPoints);

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