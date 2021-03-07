using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class WordGenerator
{
    private string[] _lines;
    string PATH = "Assets/Misc/english_wordlist.txt";
    private readonly System.Random _random = new System.Random();

    public WordGenerator()
    {
        var timer = new Stopwatch();
        timer.Start();
        this.LoadWordFile(PATH);
        timer.Stop();

        System.TimeSpan timeTaken = timer.Elapsed;
        UnityEngine.Debug.Log("Wordlist load time taken: " + timeTaken.ToString(@"m\:ss\.fff"));

    }

    private void LoadWordFile(string path)
    {
        _lines = System.IO.File.ReadAllLines(path);
    }

    public string GetNextWord()
    {
        return _lines[_random.Next(_lines.Length)];
        //return "hypocrater";
    }
}