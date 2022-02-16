using UnityEngine;

public class CameraTargetMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distance = 1;
    private float _startSpeed;
    private Vector3 _direction;

    public void ResetSpeed() => _moveSpeed = _startSpeed;
    public void SetTarget(Transform target) => _target = target;
    public void SetDistance(float distance) => _distance = distance;
    public void ChangeSpeed(float speed) => _moveSpeed = speed;

    private void Start()
    {
        _startSpeed = _moveSpeed;
        _direction = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        transform.position = Vector3.Lerp(transform.position, _target.position + _direction * _distance, _moveSpeed);
    }
}