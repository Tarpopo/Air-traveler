using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _activeRadius;
    [SerializeField] private UnityEvent _onMove;
    private Vector2 _startPosition;

    public event UnityAction OnMove
    {
        add => _onMove.AddListener(value);
        remove => _onMove.RemoveListener(value);
    }

    [SerializeField] private UnityEvent _onTouchDown;

    public event UnityAction OnTouchDown
    {
        add => _onTouchDown.AddListener(value);
        remove => _onTouchDown.RemoveListener(value);
    }

    [SerializeField] private UnityEvent _onTouchUp;

    public event UnityAction OnTouchUp
    {
        add => _onTouchUp.AddListener(value);
        remove => _onTouchUp.RemoveListener(value);
    }

    private bool _isMouseDown;
    private Camera _camera;
    public bool IsMove => Input.GetMouseButton(0);
    public Vector2 TouchPosition => Input.mousePosition;
    public float Distance => Vector2.Distance(_startPosition, TouchPosition);
    public Vector2 TouchOnScreen => _camera.ScreenToViewportPoint(TouchPosition);
    public Vector3 MoveDirection => _startPosition - TouchPosition;
    public float Angle => AngleBetweenTwoPoints(_startPosition, TouchPosition);

    private void Start()
    {
        _camera = Camera.main;
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    
    public Vector3 GetTouchOnWorld(float Zposition)
    {
        var touchPosition = TouchPosition;
        var position = new Vector3(touchPosition.x, touchPosition.y, Zposition);
        return _camera.ScreenToWorldPoint(position);
    }

    public Vector3 GetPositionOnScreen(Vector3 position)
    {
        return _camera.WorldToScreenPoint(position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = TouchPosition;
            _onTouchDown?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            if(Distance>=_activeRadius) _onMove?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _onTouchUp?.Invoke();
            _startPosition = TouchPosition;
        }
    }
}