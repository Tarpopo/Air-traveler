using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Interfaces;
using UnityEngine;

public class ResourceCollection : MonoBehaviour, IDamageable
{
    [SerializeField] private int _resourceCount;
    [SerializeField] private int _emptyResources;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private float _spawnForce;
    [SerializeField] private Health _health;
    [SerializeField] private List<Transform> _allResources;
    [SerializeField] private ParticleSystem _resourcesClear;
    [SerializeField] private ParticleSystem _takeDamage;
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Transform _forceTransform;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private WeaponTypes weaponType;
    public WeaponTypes Item => weaponType;
    private Vector3 _forceDirection => (_forceTransform.position - _spawnPoint.position).normalized;
    private int _disableCount;
    private bool _isLessDisableCount => _allResources.Count < _disableCount;
    private Transform[] _activeObjects => _allResources.Where(item => item.gameObject.activeSelf).ToArray();
    private PunchScaleAnimation _punchScaleAnimation;
    public Transform Target => transform;

    private void Start()
    {
        _punchScaleAnimation = GetComponent<PunchScaleAnimation>();
        _health.OnDeath += DisableResource;
        _health.ResetHealth();
        _disableCount = _allResources.Count / _health.CurrentHealth;
    }

    private void DisableResource()
    {
        //_resourcesClear.Play();
        _takeDamage.transform.SetParent(null);
        foreach (var collider in _colliders) collider.enabled = false;
        foreach (var resource in _allResources) resource.gameObject.SetActive(false);
    }

    private void TryDisableChildren()
    {
        if (_allResources.Count == 0 || _isLessDisableCount) return;
        for (int i = 0; i < _disableCount; i++) _activeObjects[i].gameObject.SetActive(false);
    }

    private void SpawnResource()
    {
        for (int i = 0; i < _resourceCount; i++)
        {
            var resource = ResourcesPrefabs.Instance.SpawnResource(_resourceType, transform.position);
            resource.position = _spawnPoint.position;
            resource.GetComponent<Resource>()
                .MoveResource((Quaternion.AngleAxis(Random.Range(10, 350), Vector3.up) * _forceDirection) *
                              (_spawnForce + Random.Range(0, _spawnForce/4)), false);
        }

        for (int i = 0; i < _emptyResources; i++)
        {
            var resource = ResourcesPrefabs.Instance.SpawnResource(_resourceType, transform.position);
            resource.position = _spawnPoint.position;
            resource.GetComponent<Resource>()
                .MoveResource((Quaternion.AngleAxis(Random.Range(10, 350), Vector3.up) * _forceDirection) *
                              (_spawnForce + Random.Range(0, _spawnForce/4)), true);
        }
    }

    public bool TakeDamage(Vector3 position, int damage)
    {
        SpawnResource();
        _takeDamage.Play();
        TryDisableChildren();
        _health.ReduceHealth(1);
        _punchScaleAnimation.PlayAnimation();
        return _health.IsDeath == false;
    }
}