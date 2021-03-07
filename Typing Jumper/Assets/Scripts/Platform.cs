using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] GameObject jumpPoint;

    [Header("Letter box")]
    [SerializeField] GameObject letterBoxHolder;
    [SerializeField] float letterBoxHolderSizeMultiplier;
    [SerializeField] GameObject letterBoxPrefab;


    private GameObject[] _letterBoxes;

    public string Word { get; private set; }
    public Vector2 JumpPoint => jumpPoint.transform.position;

    public bool IsCompleted { get; private set; }

    public void Initialize(string word)
    {
        Word = word;

        _letterBoxes = GenerateLetterBoxes(word.Length);

        for (int i = 0; i < _letterBoxes.Length; i++)
        {
            _letterBoxes[i].SetActive(true);
            _letterBoxes[i].GetComponent<LetterBox>().SetLetter(word[i]);
        }
    }

    public GameObject[] GenerateLetterBoxes(int count)
    {
        var boxes = new GameObject[count];
        
        ScaleLetterBoxHolder(count);

        for (int i = 0; i < count; i++)
        {
            // instantize letterbox at parent
            boxes[i] = Instantiate(letterBoxPrefab, letterBoxHolder.transform);
        }
        return boxes;
    }

    private void ScaleLetterBoxHolder(int letterCount)
    {
        RectTransform rect = letterBoxHolder.gameObject.GetComponent<RectTransform>();

        var sizeDelta = rect.sizeDelta;
        sizeDelta.x = letterCount * letterBoxHolderSizeMultiplier;

        rect.sizeDelta = sizeDelta;
    }

    public void HighlightLetterText(int letterIndex, bool isCorrect)
    {
        _letterBoxes[letterIndex].GetComponent<LetterBox>().SetLetterColor(isCorrect);
        _letterBoxes[letterIndex].GetComponent<LetterBox>().SetBackgroundActive(false);

        if (letterIndex + 1 < _letterBoxes.Length)
        {
            _letterBoxes[letterIndex + 1].GetComponent<LetterBox>().SetBackgroundActive(true);
        }
    }
    
    public void ActivatePlatform()
    {
        _letterBoxes[0].GetComponent<LetterBox>().SetBackgroundActive(true);
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
