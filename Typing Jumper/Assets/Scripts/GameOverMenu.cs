using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Button uploadScoreButton;
    [SerializeField] ScoreManager scoreManager;

    [Header("Response")]
    [SerializeField] TMP_Text responseMessage;
    [SerializeField] Color successMessageColor;
    [SerializeField] Color errorMessageColor;

    private readonly string errorMessageEmptyInput = "Please fill in your name.";

    private readonly string successMessageScoreUploaded = "Score successfully uploaded.";
    private readonly string errorMessageScoreUploaded = "Score could not be uploaded. Try again later.";

    private readonly string errorMessageScoreAlreadyUploaded = "You have already uploaded this score!";

    private bool _uploaded = false;

    private void Start()
    {
        nameInput.onValueChanged.AddListener(OnInputFieldChanged);
        uploadScoreButton.onClick.AddListener(UploadScore);

        scoreText.text = $"Score: {scoreManager.GetScore()}";
    }

    private void UploadScore()
    {
        if (nameInput.text == null || nameInput.text == string.Empty)
        {
            ShowResponseMessage(errorMessageEmptyInput, errorMessageColor);
            return;
        }

        if (_uploaded)
        {
            ShowResponseMessage(errorMessageScoreAlreadyUploaded, errorMessageColor);
            return;
        }
        
        bool success = scoreManager.UploadScore(nameInput.text);

        if (success)
        {
            _uploaded = true;
            ShowResponseMessage(successMessageScoreUploaded, successMessageColor);
        }
        else
        {
            ShowResponseMessage(errorMessageScoreUploaded, errorMessageColor);
        }
    }

    private void ShowResponseMessage(string message, Color color)
    {
        responseMessage.gameObject.SetActive(true);
        responseMessage.text = message;
        responseMessage.color = color;
    }

    private void HideResponseMessage()
    {
        responseMessage.gameObject.SetActive(false);
    }

    public void OnInputFieldChanged(string value)
    {
        if (value == null || value == string.Empty)
        {
            ShowResponseMessage(errorMessageEmptyInput, errorMessageColor);
        }
        else
        {
            HideResponseMessage();
        }
    }
}