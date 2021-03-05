﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] GameObject jumpPoint;

    [SerializeField] GameObject letterBoxHolder;
    [SerializeField] GameObject[] letterBoxes;
    [SerializeField] GameObject letterBox;

    [SerializeField] float letterBoxHolderSizeMultiplier;

    public string Word { get; private set; }
    public Vector2 JumpPoint => jumpPoint.transform.position;

    public bool IsCompleted { get; private set; }

    public void Initialize(string word)
    {
        Word = word;

        letterBoxes = GenerateLetterBoxes(word.Length);

        for (int i = 0; i < letterBoxes.Length; i++)
        {
            letterBoxes[i].SetActive(true);
            letterBoxes[i].GetComponent<LetterBox>().SetLetter(word[i]);
        }
    }

    public GameObject[] GenerateLetterBoxes(int count)
    {
        var boxes = new GameObject[count];
        // scale letterboxholder
        var scale = letterBoxHolder.transform.localScale;
        scale.x = count * letterBoxHolderSizeMultiplier;
        letterBoxHolder.transform.localScale = scale;

        for (int i = 0; i < count; i++)
        {
            // instantize letterbox at parent
            boxes[i] = Instantiate(letterBox, letterBoxHolder.transform);
        }
        return boxes;
    }

    public void HighlightLetter(int letterIndex, bool isCorrect)
    {
        letterBoxes[letterIndex].GetComponent<LetterBox>().SetLetterBoxColor(isCorrect);
    }

    public void CompletePlatform()
    {
        IsCompleted = true;

        for (int i = 0; i < Word.Length; i++)
        {
            letterBoxes[i].SetActive(false);
        }
    }
}
