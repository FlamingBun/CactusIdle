using UnityEngine;

public class PlayerStateMachine:StateMachine
{
    public Player Player { get; }
    
    public Enemy Target { get; set; }
    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; }
    public PlayerAttackState AttackState { get; }
    public PlayerHitState HitState { get; }
    public PlayerSkillState SkillState { get; }

    public PlayerStateMachine(Player _player)
    {
        this.Player = _player;

        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);
        HitState = new PlayerHitState(this);
        SkillState = new PlayerSkillState(this);
    }
}
