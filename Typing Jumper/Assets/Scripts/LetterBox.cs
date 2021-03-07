﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI letterText;
    [SerializeField] Image background;

    [SerializeField] Color correctColor;
    [SerializeField] Color incorrectColor;

    public void SetLetter(char letter)
    {
        letterText.text = letter.ToString();
    }

    public void SetLetterBoxColor(bool isCorrect)
    {
        background.color = isCorrect ? correctColor : incorrectColor;
    }
}