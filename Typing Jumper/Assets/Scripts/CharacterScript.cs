using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] new Camera camera;
    [SerializeField] float cameraSpeed;
    [SerializeField] float defaultCameraZ;

    [Header("Character")]
    [SerializeField] GameObject character;
    [SerializeField] float characterYOffset;
    [SerializeField] float characterSpeed;
    [SerializeField] float defaultCharacterZ;

    private Vector3 _cameraTargetPosition;
    private Vector3 _characterTargetPositon;

    private bool _updatePosition = false;

    public void MoveToPosition(Vector2 position)
    {
        var currentPosition = camera.transform.position;
        currentPosition.y = position.y + 3;
        _cameraTargetPosition = currentPosition;
        _cameraTargetPosition.z = defaultCameraZ;

        position.y += characterYOffset;
        _characterTargetPositon = position;
        _characterTargetPositon.z = defaultCharacterZ;

        _updatePosition = true;
    }

    private void Update()
    {
        if (_updatePosition == false)
            return;

        if (_cameraTargetPosition == camera.transform.position
            && _characterTargetPositon == character.transform.position)
        {
            _updatePosition = false;
            return;
        }

        camera.transform.position = Vector3.MoveTowards(camera.transform.position, _cameraTargetPosition, cameraSpeed * Time.deltaTime);
        character.transform.position = Vector3.MoveTowards(character.transform.position, _characterTargetPositon, characterSpeed * Time.deltaTime);
    }
}
