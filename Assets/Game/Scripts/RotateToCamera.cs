using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Transform _camera;
    private void Start() => _camera = Camera.main.transform;
    public void Rotate() => transform.LookAt(_camera);
}