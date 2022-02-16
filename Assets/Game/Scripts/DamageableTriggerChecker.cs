using System.Collections.Generic;
using Game.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class DamageableTriggerChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDamageableChange;

    public event UnityAction OnDamageableChange
    {
        add => _onDamageableChange.AddListener(value);
        remove => _onDamageableChange.RemoveListener(value);
    }

    private readonly List<IDamageable> _damageables = new List<IDamageable>();
    private IDamageable _current;
    public Vector3 TargetDirection => (_current.Target.position - transform.position).normalized;
    public WeaponTypes GetWeaponType => _current.Item;

    public bool CheckDamageable() => _damageables.Count != 0;

    public void TryApplyDamage(int damage)
    {
        if (_current.TakeDamage(transform.position, damage)) return;
        _damageables.Remove(_current);
        TrySetCurrent();
    }

    public void ResetAll()
    {
        _damageables.Clear();
        _current = null;
    }

    private void TrySetCurrent()
    {
        if (CheckDamageable() == false) return;
        _current = _damageables[0];
        _onDamageableChange?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable) == false) return;
        _damageables.Add(damageable);
        if (_damageables.Count == 1) TrySetCurrent();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable) == false) return;
        _damageables.Remove(damageable);
        if (_current.Equals(damageable)) TrySetCurrent();
    }
}