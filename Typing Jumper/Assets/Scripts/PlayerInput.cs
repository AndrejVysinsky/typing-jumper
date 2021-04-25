using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlatformGenerator platformGenerator;
    [SerializeField] CharacterScript characterScript;
    [SerializeField] HealthScript healthScript;
    [SerializeField] GameObject pauseMenu;

    private int[] values;
    private bool[] keys;

    private Platform _activePlatform;
    private int _currentletterIndex;
    private int _incorrectLetterCount;

    private bool _isGameOver = false;
    private bool _isPaused = false;

    private void Start()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        keys = new bool[values.Length];
    }

    void Update()
    {
        if (_isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (_isPaused)
            return;

        if (Input.anyKeyDown)
        {
            HandleKeyboardInput();
        }
    }

    private void HandleKeyboardInput()
    {
        if (_activePlatform == null)
        {
            _activePlatform = platformGenerator.GetNextPlatform();
        }

        string userInput = Input.inputString;

        if (userInput.Length == 0)
            return;

        bool isLetterCorrect = userInput[0] == _activePlatform.Word[_currentletterIndex];

        if (!isLetterCorrect)
        {
            int remainingHealthPoints = healthScript.RemoveHealthPoint();

            if (remainingHealthPoints == 0)
            {
                _isGameOver = true;
                return;
            }
            _incorrectLetterCount++;
        }

        _activePlatform.HighlightLetterText(_currentletterIndex, isLetterCorrect);

        _currentletterIndex++;

        if (_currentletterIndex == _activePlatform.Word.Length)
        {
            PlatformCompleted();
        }
    }

    private void PlatformCompleted()
    {
        characterScript.MoveToPosition(_activePlatform.JumpPoint);

        platformGenerator.CompletePlatform(_activePlatform, _incorrectLetterCount);

        _activePlatform = platformGenerator.GetNextPlatform();

        _currentletterIndex = 0;
        _incorrectLetterCount = 0;
    }

    public void PauseGame()
    {
        _isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _isPaused = false;
        pauseMenu.SetActive(false);
    }
}
