using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _closeRadius = 0.1f;
    public bool IsClosePoint(Vector3 target) => Vector3.Distance(_currentPoint, target) <= _closeRadius;
    private Vector3 _currentPoint;

    public Vector3 GetRandomPoint() => _currentPoint = _points[Random.Range(0, _points.Length)].position;
}