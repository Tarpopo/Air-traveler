using System;
using UnityEngine;

public class CameraTargetChanger : MonoBehaviour
{
    [SerializeField] private CameraTargetMover _cameraTargetMover;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private void Start() => _cameraTargetMover = FindObjectOfType<CameraTargetMover>();

    public void ChangeTarget()
    {
        _cameraTargetMover.SetTarget(_target);
        _cameraTargetMover.ChangeSpeed(_speed);
        _cameraTargetMover.SetDistance(_distance);
    }
}