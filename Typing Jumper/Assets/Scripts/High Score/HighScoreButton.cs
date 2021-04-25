using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighScoreButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Components")]
    [SerializeField] Image buttonImage;
    [SerializeField] TMP_Text buttonText;

    [Header("Active state")]
    [SerializeField] Color activeTextColor;
    [SerializeField] Color activeButtonColor;

    [Header("Inactive state")]
    [SerializeField] Color inactiveTextColor;
    [SerializeField] Color inactiveButtonColor;

    [Header("Highlight state")]
    [SerializeField] Color highlightTextColor;
    [SerializeField] Color highlightButtonColor;

    [Header("Misc")]
    [SerializeField] HighScoreScreen highScoreScreen;
    [SerializeField] DifficultyEnum difficulty;

    public DifficultyEnum Difficulty => difficulty;

    public void OnPointerDown(PointerEventData eventData)
    {
        highScoreScreen.HighScoreButtonClicked(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highScoreScreen.IsHighScoreButtonClicked(this))
            return;

        HighlightButton();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (highScoreScreen.IsHighScoreButtonClicked(this))
            return;

        DeactivateButton();
    }

    public void ActivateButton()
    {
        buttonImage.color = activeButtonColor;
        buttonText.color = activeTextColor;
    }

    public void DeactivateButton()
    {
        buttonImage.color = inactiveButtonColor;
        buttonText.color = inactiveTextColor;
    }

    private void HighlightButton()
    {
        buttonImage.color = highlightButtonColor;
        buttonText.color = highlightTextColor;
    }
}