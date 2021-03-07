using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlatformGenerator platformGenerator;
    [SerializeField] CameraScript cameraScript;

    private int[] values;
    private bool[] keys;

    private Platform _activePlatform;
    private int _currentletterIndex;

    private void Start()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        keys = new bool[values.Length];
    }

    void Update()
    {
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

        _activePlatform.HighlightLetterText(_currentletterIndex, isLetterCorrect);

        _currentletterIndex++;

        if (_currentletterIndex == _activePlatform.Word.Length)
        {
            PlatformCompleted();
        }
    }

    private void PlatformCompleted()
    {
        cameraScript.MoveCamera(_activePlatform.JumpPoint);

        platformGenerator.CompletePlatform(_activePlatform);

        _activePlatform = platformGenerator.GetNextPlatform();

        _currentletterIndex = 0;
    }
}
