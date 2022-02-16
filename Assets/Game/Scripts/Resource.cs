using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    [SerializeField] private UnityEvent _onMoveEnd;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private float _delay;
    [SerializeField] private float _flyDelay;
    [SerializeField] private int _frames = 10;
    [SerializeField] private bool _isWeapon;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void PlayMove() => MoveResource(Vector3.up, true);

    public void MoveResource(Vector3 startForceDirection, bool isEmpty) =>
        StartCoroutine(Move(startForceDirection, isEmpty));

    private IEnumerator Move(Vector3 forceDirection, bool isEmpty)
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
        _rigidbody.velocity = forceDirection;
        yield return new WaitForSeconds(_delay + Random.Range(0, _flyDelay));
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        var delta = (ResourceCollector.Instance.CollectPosition - transform.position) / _frames;
        for (int i = 0; i < _frames; i++)
        {
            transform.position += delta;
            yield return null;
        }
        _onMoveEnd?.Invoke();
        if (_isWeapon)
        {
            gameObject.SetActive(false);
            yield break;
        }
        if (isEmpty)
        {
            ManagerPool.Instance.Despawn(PoolType.Entities, gameObject);
            yield break;
        }

        ResourceCollector.Instance.AddResource(_resourceType);
        ResourceCollector.Instance.BackStack.AddToStack(_resourceType, transform);
        transform.eulerAngles = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}