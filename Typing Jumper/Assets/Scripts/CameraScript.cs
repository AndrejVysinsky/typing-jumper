using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Camera camera;

    public void MoveCamera(Vector2 position)
    {
        var currentPosition = camera.transform.position;

        currentPosition.y = position.y + 3;

        camera.transform.position = currentPosition;
    }
}
