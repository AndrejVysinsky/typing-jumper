using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] GameObject jumpPoint;

    [SerializeField] GameObject letterBoxHolder;
    [SerializeField] List<GameObject> letterBoxes;

    [SerializeField] float letterBoxHolderSizeMultiplier;

    public string Word { get; private set; }
    public Vector2 JumpPoint => jumpPoint.transform.position;

    public bool IsCompleted { get; private set; }

    public void Initialize(string word)
    {
        Word = word;

        for (int i = 0; i < letterBoxes.Count; i++)
        {
            if (i < word.Length)
            {
                letterBoxes[i].SetActive(true);
                letterBoxes[i].GetComponent<LetterBox>().SetLetter(word[i]);
            }
            else
            {
                letterBoxes[i].SetActive(false);
            }
        }

        var scale = letterBoxHolder.transform.localScale;
        scale.x = word.Length * letterBoxHolderSizeMultiplier;
        letterBoxHolder.transform.localScale = scale;
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
