using UnityEngine;

public class PlayerCameraManager : PlayerInputReciever
{
    [SerializeField] private Camera _camera;
    [SerializeField][Range(-90, 0)] private float _minCameraPitch;
    [SerializeField][Range(0, 90)] private float _maxCameraPitch;
    [SerializeField] private float _pitchSpeed;

    private float _currentPitch = 0f;

    private new void OnEnable()
    {
        base.OnEnable();

        //Temp here - move to some cursor manager class down the line
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected override void ListenForInput()
    {
        _input.OnVerticalMouseMove += PitchCamera;
    }

    protected override void UnlistenForInput()
    {
        _input.OnVerticalMouseMove -= PitchCamera;
    }

    private void PitchCamera(float delta)
    {
        float newPitch = _currentPitch + (delta * _pitchSpeed);
        _currentPitch = Mathf.Clamp(newPitch, _minCameraPitch, _maxCameraPitch);

        Vector3 currentRotation = _camera.transform.localRotation.eulerAngles;
        Vector3 newEulerRotation = new Vector3(_currentPitch, currentRotation.y, currentRotation.z);

        _camera.transform.localRotation = Quaternion.Euler(newEulerRotation);
    }
}
