using UnityEngine;

public class PlayerCameraManager : InputReviever
{
    [SerializeField] private Camera _camera;
    [SerializeField][Range(-90, 0)] private float _minCameraPitch;
    [SerializeField][Range(0, 90)] private float _maxCameraPitch;
    [SerializeField] private float _pitchSpeed;

    private float _currentPitch = 0f;

    protected override void ListenForInput()
    {
        _input.OnVerticalMouseMove += PitchCamera;
    }

    protected override void UnlistenForInput()
    {
        _input.OnVerticalMouseMove -= PitchCamera;
    }

    /// <summary>
    /// Pitch camera up and down
    /// </summary>
    /// <param name="delta">value to pitch camera by</param>
    private void PitchCamera(float delta)
    {
        //Update current pitch, clamped between min and max camera pitch
        float newPitch = _currentPitch + (delta * _pitchSpeed);
        _currentPitch = Mathf.Clamp(newPitch, _minCameraPitch, _maxCameraPitch);

        //Calculate the new rotation for the camera
        Vector3 currentRotation = _camera.transform.localRotation.eulerAngles;
        Vector3 newEulerRotation = new Vector3(_currentPitch, currentRotation.y, currentRotation.z);

        //Update the cameras rotation
        _camera.transform.localRotation = Quaternion.Euler(newEulerRotation);
    }
}
