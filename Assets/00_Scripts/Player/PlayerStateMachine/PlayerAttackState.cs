public class PlayerAttackState:PlayerBaseState
{
    private bool alreadyApplyForce;
    private bool alreadyAppliedDealing;
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        stateMachine.Player.PlayerCondition.ChangeMoveSpeed(0f);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        alreadyApplyForce = false;
        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        stateMachine.Player.PlayerCondition.SetPreviousMoveSpeed();
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        ForceMove();
        
        
        float normalizeTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        
        if (normalizeTime < 1f)
        {
            if (normalizeTime >= stateMachine.Player.Weapon.ForceTransitionTime)
            {
                TryApplyForce();
            }
            if (!alreadyAppliedDealing && normalizeTime >= stateMachine.Player.Weapon.Dealing_Start_TransitionTime)
            {
                stateMachine.Player.Weapon.Fire();
                alreadyAppliedDealing = true;
            }
        }
        else
        {
            if (HasTarget())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
    }
    void TryApplyForce()
    {
        if (alreadyApplyForce) return;
        alreadyApplyForce = true;

        // ForceReceiver에서 많은 값을 관리하므로 이전의 값이 현재에 영향을 주지 않게 하기 위해 Reset();
        stateMachine.Player.ForceReceiver.Reset();
        
        // 공격할 때 앞으로 나아가기 때문에 Vector3.forward를 곱한다.
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * stateMachine.Player.Weapon.Force);
    }
}
