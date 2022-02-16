using UnityEngine;

public class Move : State<PlayerData>
{
    public Move(PlayerData data, StateMachine<PlayerData> stateMachine) : base(data, stateMachine)
    {
    }

    public override bool IsStatePlay() => Data.PlayerInput.IsMove;

    public override void PhysicsUpdate()
    {
        if (Data.PlayerInput.IsMove == false)
        {
            Machine.ChangeState<Idle>();
            return;
        }

        Data.AnimationComponent.PlayAnimation(UnitAnimations.Run);
        Data.Transform.rotation = Quaternion.AngleAxis(-Data.PlayerInput.Angle + Data.AngleOffset, Vector3.up);
        if (Data.SurfaceMovement.CheckGround() == false) return;
        Data.Rigidbody.MovePosition(Data.Rigidbody.position +
                                    Data.SurfaceMovement.Project(Data.Transform.forward) * Data.MoveSpeed);
    }
    
}