using System.Collections.Generic;
using UnityEngine;

public class HighScoreScreen : MonoBehaviour
{
    [SerializeField] List<HighScoreButton> highScoreButtons;

    private HighScoreButton activeHighScoreButton;

    private void Start()
    {
        for (int i = 0; i < highScoreButtons.Count; i++)
        {
            if (i == 0)
            {
                activeHighScoreButton = highScoreButtons[i];
                activeHighScoreButton.ActivateButton();
                continue;
            }

            highScoreButtons[i].DeactivateButton();
        }
    }

    public void HighScoreButtonClicked(HighScoreButton highScoreButton)
    {
        activeHighScoreButton.DeactivateButton();
        activeHighScoreButton = highScoreButton;
        activeHighScoreButton.ActivateButton();

        LoadHighScores(activeHighScoreButton.Difficulty);
    }

    public bool IsHighScoreButtonClicked(HighScoreButton highScoreButton)
    {
        return activeHighScoreButton == highScoreButton;
    }

    private void LoadHighScores(DifficultyEnum difficulty)
    {
        //send API request for high scores
    }
}
