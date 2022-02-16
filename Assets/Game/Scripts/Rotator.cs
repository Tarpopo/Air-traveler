using System.Collections;
using UnityEngine;

[System.Serializable]
public class Rotator : MonoBehaviour
{
    [SerializeField] private int _rotateSpeed;
    [SerializeField] private Transform _propeller;
    [SerializeField] private bool _startOnEnable;

    public void SetRotateState(bool isActive)
    {
        if (isActive) StartCoroutine(Rotate());
        else StopAllCoroutines();
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            _propeller.Rotate(Vector3.forward, _rotateSpeed);
            yield return null;
        }
    }

    private void OnEnable() => SetRotateState(_startOnEnable);
    private void OnDisable() => StopAllCoroutines();
}