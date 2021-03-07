﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] float platformHeightDiff;
    [SerializeField] float platformMinX;
    [SerializeField] float platformMaxX;
    [SerializeField] GameObject platformPrefab;

    private List<Platform> _platformList;
    private int _realPlatformCount;
    private WordGenerator _wordGenerator;

    private void Start()
    {
        _platformList = new List<Platform>();
        _realPlatformCount = 0;
        _wordGenerator = new WordGenerator();
    }

    private void Update()
    {
        if (_platformList.Count < 10)
        {
            GeneratePlatform();
        }
    }

    private void GeneratePlatform()
    {
        var position = new Vector2(Random.Range(platformMinX, platformMaxX), platformHeightDiff * _realPlatformCount);

        var platformObject = Instantiate(platformPrefab, transform);
        platformObject.transform.position = position;

        var platform = platformObject.GetComponent<Platform>();
        platform.Initialize(_wordGenerator.GetNextWord());

        _platformList.Add(platform);

        _realPlatformCount++;
    }

    public Platform GetNextPlatform()
    {
        if (_platformList.Count == 0)
        {
            GeneratePlatform();
        }

        for (int i = 0; i < _platformList.Count; i++)
        {
            if (_platformList[i].IsCompleted)
            {
                continue;
            }
            return _platformList[i];
        }

        return null;
    }

    public void CompletePlatform(Platform platform)
    {
        platform.CompletePlatform();

        if (_platformList.IndexOf(platform) >= 3)
        {
            Destroy(_platformList[0].gameObject);
            _platformList.RemoveAt(0);
        }
    }
}