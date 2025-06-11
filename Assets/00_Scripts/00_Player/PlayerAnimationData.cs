using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string moveParameterName = "Move";
    
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string hitParameterName = "Hit";
    [SerializeField] private string dieParameterName = "Die";
    
    public int IdleParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        MoveParameterHash = Animator.StringToHash(moveParameterName);
        
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        
        HitParameterHash = Animator.StringToHash(hitParameterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
        Animator animator = new Animator();
    }
}
