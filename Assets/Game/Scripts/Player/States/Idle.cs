using UnityEngine;

public class Idle : State<PlayerData>
{
    private readonly Timer _timer;

    public Idle(PlayerData data, StateMachine<PlayerData> stateMachine) : base(data, stateMachine) =>
        _timer = new Timer();

    public override void Enter()
    {
        _timer.StartTimer(Data.IdleTime, StartIdleTimer);
        Data.AnimationComponent.PlayAnimation(UnitAnimations.Idle);
    }

    public override void PhysicsUpdate()
    {
        if (Data.DamageableChecker.CheckDamageable() == false) return;
        Machine.ChangeState<Attack>();
    }

    public override void LogicUpdate() => _timer.UpdateTimer();

    private void StartIdleTimer()
    {
        Data.AnimationComponent.PlayRandomAnimation();
        _timer.StartTimer(Data.IdleTime, StartIdleTimer);
    }
}