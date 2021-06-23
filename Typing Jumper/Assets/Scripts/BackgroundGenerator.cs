using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> backgroundParts;

    [Header("Background Variants")]
    [SerializeField] Sprite groundSprite;
    [SerializeField] List<Sprite> lowHeightSprites;

    private Camera _mainCamera;

    /*
     * vzdy 3 casti:
     * na zaciatku je 1x zem + 2x random low height
     * ked vrch kamery (vrch obrazovky) sa dotkne najvrchnejsej casti -> vtedy uz spodnu by nemalo byt vidiet
     * prehodi na spodna na najvrchnejsiu -> a prehodi sa random sprite
     */

    void Start()
    {
        _mainCamera = Camera.main;

        for (int i = 0; i < backgroundParts.Count; i++)
        {
            backgroundParts[i].transform.position = new Vector2(0, i * _mainCamera.orthographicSize * 2);
        }

        backgroundParts[0].GetComponent<SpriteRenderer>().sprite = groundSprite;
        backgroundParts[1].GetComponent<SpriteRenderer>().sprite = lowHeightSprites[Random.Range(0, lowHeightSprites.Count)];
        backgroundParts[2].GetComponent<SpriteRenderer>().sprite = lowHeightSprites[Random.Range(0, lowHeightSprites.Count)];
    }

    void Update()
    {
        float cameraHeighestPoint = _mainCamera.transform.position.y + _mainCamera.orthographicSize;

        float heighestBackgroundPartLowestPoint = backgroundParts[backgroundParts.Count - 1].transform.position.y - _mainCamera.orthographicSize;

        if (cameraHeighestPoint <= heighestBackgroundPartLowestPoint)
        {
            return;
        }

        var lowestPart = backgroundParts[0];
        backgroundParts.RemoveAt(0);

        //daj ju vyssie
        var heighestPosition = backgroundParts[backgroundParts.Count - 1].transform.position;
        heighestPosition.y += _mainCamera.orthographicSize * 2;
        lowestPart.transform.position = heighestPosition;

        //vygeneruj novy obrazok
        lowestPart.GetComponent<SpriteRenderer>().sprite = lowHeightSprites[Random.Range(0, lowHeightSprites.Count)];

        backgroundParts.Add(lowestPart);
    }
}
