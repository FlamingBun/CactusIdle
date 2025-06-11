
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private Transform attackPoint;
    [SerializeField]private Transform RWeaponPosition;
    [SerializeField]private Transform LWeaponPosition;
    private int power;
    private float knockBack;

    private Player player;
    private PlayerWeaponSO weaponSO;
    
    public float AttackRange { get; private set; }
    public float AttackRate { get; private set; }
    
    public float Force { get; private set; }
    public float ForceTransitionTime { get; private set; }
    
    public float Dealing_Start_TransitionTime { get; private set; }
    public float Dealing_End_TransitionTime { get; private set; }
    

    public void Init(Player _player, PlayerWeaponSO _weaponSO)
    {
        player = _player;
        SetWeapon(_weaponSO);
    }
    
    public void SetWeapon(PlayerWeaponSO _weaponSO)
    {
        weaponSO = _weaponSO;
        SetPower();
        this.knockBack = weaponSO.knockBackForce;
        this.AttackRange = weaponSO.attackRange;
        this.Force = weaponSO.force;
        this.ForceTransitionTime = weaponSO.forceTransitionTime;
        this.Dealing_Start_TransitionTime = weaponSO.dealing_Start_TransitionTime;
        this.Dealing_End_TransitionTime = weaponSO.dealing_End_TransitionTime;
    }

    public void SetPower()
    {
        this.power =weaponSO.power + player.Condition.Power;
        this.AttackRate = weaponSO.attackRate + player.Condition.AttackRate;
    }
    
    public void Fire()
    {
        ObjectPoolManager.Instance.GetObject(weaponSO.projectile, attackPoint.position,Quaternion.identity).GetComponent<Projectile>().Initialize(weaponSO, power, player.transform.forward);
    }

}