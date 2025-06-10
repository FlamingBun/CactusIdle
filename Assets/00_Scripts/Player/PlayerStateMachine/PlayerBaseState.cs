using UnityEngine;

public class PlayerBaseState: IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly Player player;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        player = stateMachine.Player;
    }
    
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
    
    public virtual void Update()
    {
        Move();
    }
    
    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }
    
    private void Move()
    {
        if (!HasTarget()) return;
        
        Vector3 movementDirection = GetMovementDirection();
        
        Move(movementDirection);
        
        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).normalized;
        return dir;
    }

    private void Move(Vector3 direction)
    {
        // 공격 범위 안에 들어왔는지 확인
        if (Vector3.Distance(stateMachine.Target.transform.position, stateMachine.Player.transform.position) < stateMachine.Player.Weapon.AttackRange)
        {
            return;
        }

        
        stateMachine.Player.Controller.Move(((direction * stateMachine.MoveSpeed) + stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime);
    }
    

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
     
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }
    
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);
        
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool HasTarget()
    {
        if (stateMachine.Target == null) return false;

        if (stateMachine.Target.gameObject.activeSelf && !stateMachine.Target.Condition.IsDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void FindTarget()
    {
        stateMachine.Target = GameManager.Instance.EnemyManager.GetNearestEnemyFromPlayer();
    }
}
