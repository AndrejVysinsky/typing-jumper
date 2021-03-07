using TMPro;
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

        var rect = gameObject.GetComponent<RectTransform>();

        var size = rect.sizeDelta;
        size.x = letterText.preferredWidth;

        rect.sizeDelta = size;
    }

    public void SetLetterColor(bool isCorrect)
    {
        letterText.color = isCorrect ? correctColor : incorrectColor;
    }

    public void SetBackgroundActive(bool isActive)
    {
        background.gameObject.SetActive(isActive);
    }
}
