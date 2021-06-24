using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class WordGenerator
{
    private string[] _lines;
    string PATH = "Assets/Resources/english_wordlist.txt";
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
        _lines = Resources.Load<TextAsset>("english_wordlist").text.Split('\n');
    }

    public string GetNextWord()
    {
        string word = _lines[_random.Next(_lines.Length)];

        while (word.Length > 11)
        {
            word = _lines[_random.Next(_lines.Length)];
        }
        return word;
    }
}
