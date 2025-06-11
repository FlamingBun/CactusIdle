public class PlayerAttackState:PlayerBaseState
{
    private bool alreadyApplyForce;
    private bool alreadyFirstFire;
    private bool alreadySecondFire;
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        stateMachine.Player.PlayerCondition.ChangeMoveSpeed(0f);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        alreadyApplyForce = false;
        alreadyFirstFire = false;
        alreadySecondFire = false;
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
            if (!alreadyFirstFire && normalizeTime >= stateMachine.Player.Weapon.Dealing_Start_TransitionTime)
            {
                stateMachine.Player.Weapon.Fire();
                alreadyFirstFire = true;
            }

            if (!alreadySecondFire && normalizeTime >= 0.6f)
            {
                stateMachine.Player.Weapon.Fire();
                alreadySecondFire = true;
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
        
        stateMachine.Player.ForceReceiver.Reset();
        
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * stateMachine.Player.Weapon.Force);
    }
}
