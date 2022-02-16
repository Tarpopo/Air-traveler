using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    [SerializeField] private UnityEvent _onMoveEnd;
    [SerializeField] private GameObject _bridge;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private int _moveframes;
    [SerializeField] private int _rotationFrames;
    [SerializeField] private bool _restartOnEndMove;
    private CameraTargetMover _cameraTargetMover;
    private Transform _playerTransform;
    private Vector3 _startPosition;

    private void Start()
    {
        _playerTransform = FindObjectOfType<Player>().transform;
        _cameraTargetMover = FindObjectOfType<CameraTargetMover>();
        if (_restartOnEndMove == false) return;
        var levelManager = FindObjectOfType<LevelManager>();
        _onMoveEnd.AddListener(levelManager.LoadLevel);
    }

    public void MoveShip()
    {
        _bridge.SetActive(false);
        _playerTransform.SetParent(transform);
        _cameraTargetMover.ChangeSpeed(10);
        StartCoroutine(CorroutinesKid.EulerRotate(transform, _endPoint.eulerAngles, _rotationFrames,
            () =>
            {
                StartCoroutine(CorroutinesKid.Move(transform, _endPoint.position, _moveframes,
                    () =>
                    {
                        ResetShip();
                        _onMoveEnd?.Invoke();
                    }));
            }));
    }

    private void ResetShip()
    {
        _playerTransform.SetParent(null);
        //transform.position = _startPosition;
        _bridge.SetActive(true);
        _cameraTargetMover.ResetSpeed();
    }
}