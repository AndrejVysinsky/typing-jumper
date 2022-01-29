using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighScoreButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    private bool _isActive = false;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isActive)
            return;

        HighlightButton();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isActive)
            return;

        DeactivateButton();
    }

    public void ActivateButton()
    {
        _isActive = true;
        buttonImage.color = activeButtonColor;
        buttonText.color = activeTextColor;
    }

    public void DeactivateButton()
    {
        _isActive = false;
        buttonImage.color = inactiveButtonColor;
        buttonText.color = inactiveTextColor;
    }

    private void HighlightButton()
    {
        buttonImage.color = highlightButtonColor;
        buttonText.color = highlightTextColor;
    }
}