using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerCondition))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(ForceReceiver))]
[RequireComponent(typeof(Weapon))]
public class Player : MonoBehaviour
{
    public Transform CameraPoint;
    public PlayerCondition PlayerCondition { get; private set; }
    
    #region Animation
    public PlayerAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    #endregion Animation
    
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    
    private PlayerStateMachine stateMachine;
    public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData = new PlayerAnimationData();
        PlayerCondition =GetComponent<PlayerCondition>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        Weapon = GetComponent<Weapon>();
        
        
    }

    public void Initialize(PlayerDataSO playerData)
    {
        AnimationData.Initialize();
        
        PlayerCondition.Init(playerData.playerStat);
        Weapon.Init(this,playerData.weaponSO);
        
        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
