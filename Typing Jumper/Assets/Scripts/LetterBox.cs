using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI letterText;
    [SerializeField] Image background;
    [SerializeField] float extraSizeOnX;

    [SerializeField] Color correctColor;
    [SerializeField] Color incorrectColor;

    public void SetLetter(char letter)
    {
        letterText.text = letter.ToString();

        var rect = gameObject.GetComponent<RectTransform>();

        var size = rect.sizeDelta;
        size.x = letterText.preferredWidth + extraSizeOnX;
        rect.sizeDelta = size;

        size.y = rect.sizeDelta.y;
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
