public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.Condition.ChangeMoveSpeed(0f);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        stateMachine.Player.Condition.SetPreviousMoveSpeed();
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (HasTarget())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
        else
        {
            FindTarget();
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
