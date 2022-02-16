using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private HealthUI _healthUI;
    [SerializeField] private int _health = 3;
    [SerializeField] private int _delayFrames;
    [SerializeField] private int _healthFrames;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsDeath => _currentHealth <= 0;

    public event UnityAction OnDeath
    {
        add => _onDeath.AddListener(value);
        remove => _onDeath.RemoveListener(value);
    }


    public void ResetHealth()
    {
        _currentHealth = _health;
        if (_healthUI.IsHaveUI == false) return;
        _healthUI.UpdateUI(1, false);
    }

    public void ReduceHealth(int value)
    {
        if (IsDeath) return;
        _currentHealth -= value;
        TryUpdateUI(_currentHealth * ((float) 1 / _health), true);
        if (IsDeath) _onDeath?.Invoke();
    }

    private IEnumerator UpdateHealth()
    {
        for (int i = 0; i < _delayFrames; i++)
        {
            _healthUI.UpdateCanvas();
            yield return null;
        }

        var delta = (_health - _currentHealth) / _healthFrames;
        for (int i = 0; i < _healthFrames; i++)
        {
            _currentHealth += delta;
            _healthUI.UpdateUI(_currentHealth * (float) 1 / _health, true);
            yield return null;
        }

        _healthUI.UpdateUI(1, false);
    }

    private void TryUpdateUI(float value, bool showUI)
    {
        if (_healthUI.IsHaveUI == false) return;
        _healthUI.UpdateUI(value, showUI);
        StopAllCoroutines();
        StartCoroutine(UpdateHealth());
    }
}