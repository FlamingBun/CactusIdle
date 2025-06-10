public class PlayerChasingState:PlayerBaseState
{
    public PlayerChasingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();
        
        if (!stateMachine.Target.gameObject.activeSelf || stateMachine.Target.Condition.IsDead)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        
        if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);    
        }
    }
    
    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Player.Weapon.AttackRange * stateMachine.Player.Weapon.AttackRange;
    }
}
