using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text scoreText;
    private int _score = 0;
    private float a_param = 1f;
    private float b_param = 1.3f;
    private const int ZERO_PREFIX_COUNT = 5;

    public void EvaluateScore(string word, int mistakes)
    {
        // https://www.desmos.com/calculator/3fisjexbvp
        double complexity = a_param * Math.Pow(b_param, word.Length);
        double reward = Math.Round(complexity - (mistakes / word.Length) * word.Length);
        this._score += (int) reward;
        //Debug.Log("Added to score:" + reward);
        this.UpdateScore();
    }

    void UpdateScore()
    {
        int digitCount = (int)Math.Floor(Math.Log10(_score) + 1);
        string balanceString = "SCORE: ";

        for (int i = 0; i < ZERO_PREFIX_COUNT - digitCount; i++)
        {
            balanceString += "0";
        }
        balanceString += _score.ToString();
        if (scoreText != null)
        {
            this.scoreText.text = balanceString;
        }
    }

    public int GetScore()
    {
        return _score;
    }
}
