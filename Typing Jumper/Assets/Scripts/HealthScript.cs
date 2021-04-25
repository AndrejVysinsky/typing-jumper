using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] List<Image> healthPoints;

    public void Initialize(int numberOfHealthPoints)
    {
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