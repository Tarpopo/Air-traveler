using Game.Scripts.Enemy.States;
using Game.Scripts.Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData _enemyData;
    public WeaponTypes Item => WeaponTypes.Sword;
    public Transform Target => transform;
    private StateMachine<EnemyData> _stateMachine;
    private IDamageable _lastDamageable;

    private void Start()
    {
        _stateMachine = new StateMachine<EnemyData>();
        _stateMachine.AddState(new EnemyAttack(_enemyData, _stateMachine));
        _stateMachine.AddState(new EnemyIdle(_enemyData, _stateMachine));
        _stateMachine.AddState(new EnemyWalk(_enemyData, _stateMachine));
        _stateMachine.AddState(new EnemyMoveToPlayer(_enemyData, _stateMachine));
        _stateMachine.Initialize<EnemyIdle>();
        _enemyData.AnimationComponent.SetParameters();
        _enemyData.Health.ResetHealth();
        _enemyData.Health.OnDeath += () =>
        {
            ParticleManager.Instance.PlayParticle(ParticleTypes.Death, transform.position, Vector3.zero,
                Vector3.one * 0.2f, null);
            Destroy(gameObject);
        };
        _enemyData.VisibleZoneChecker.SetTarget(FindObjectOfType<Player>().transform);
    }

    private void Update() => _stateMachine.CurrentState.LogicUpdate();

    private void FixedUpdate() => _stateMachine.CurrentState.PhysicsUpdate();

    public void ApplyDamage()
    {
        _enemyData.VisibleZoneChecker.GetDamageable().TakeDamage(transform.position, _enemyData.Damage);
    }

    public bool TakeDamage(Vector3 position, int damage)
    {
        _enemyData.Health.ReduceHealth(damage);
        return _enemyData.Health.IsDeath == false;
    }
}