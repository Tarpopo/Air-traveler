using UnityEngine;

public class ObstaclesChecker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rayDistance;
    [SerializeField] private LayerMask _obstaceLayers;
    private Obstacle _obstacle;
    private RaycastHit _hit;

    private void Update() => CheckObstacles();

    private void CheckObstacles()
    {
        var ray = new Ray(transform.position, _target.position-transform.position);
        if (Physics.Raycast(ray, out var hit, _rayDistance, _obstaceLayers) == false)
        {
            if (_obstacle != null) _obstacle.SetObstacle(true);
            _obstacle = null;
            return;
        }
        if (hit.collider.TryGetComponent<Obstacle>(out var component) == false || _obstacle==component) return;
        if (_obstacle != null) _obstacle.SetObstacle(true);
        _obstacle = component;
        _obstacle.SetObstacle(false);
    }
    
}