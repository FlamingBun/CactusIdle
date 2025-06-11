public class PlayerSkillState:PlayerBaseState
{
    public PlayerSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.Player.PlayerCondition.ChangeMoveSpeed(0f);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
