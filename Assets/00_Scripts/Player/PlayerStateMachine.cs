using UnityEngine;

public class PlayerStateMachine:StateMachine
{
    public Player Player { get; }
    
    public Vector2 MovementInput { get; set; }
    public float MoveSpeed { get; private set; }
    public float RotationDamping { get; private set; }

    public Enemy Target { get; set; }
    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; }
    public PlayerAttackState AttackState { get; }
    public PlayerHitState HitState { get; }
    public PlayerSkillState SkillState { get; }

    public void OnEnable()
    {
        Player.PlayerCondition.OnSpeedChange += OnSpeedChange;
    }

    public void OnDisable()
    {
        Player.PlayerCondition.OnSpeedChange -= OnSpeedChange;
    }

    public PlayerStateMachine(Player _player)
    {
        this.Player = _player;

        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);
        HitState = new PlayerHitState(this);
        SkillState = new PlayerSkillState(this);
        
        MoveSpeed = Player.PlayerCondition.CurrentMoveSpeed;
        RotationDamping = Player.PlayerCondition.CurrentRotationDamping;
        
        Target = GameManager.Instance.EnemyManager.GetNearestEnemyFromPlayer();
    }

    private void OnSpeedChange(float moveSpeed, float rotationDamping)
    {
        MoveSpeed = moveSpeed;
        RotationDamping = rotationDamping;
    }
}
