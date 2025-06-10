
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private Transform attackPoint;
    [SerializeField]private Transform RWeaponPosition;
    [SerializeField]private Transform LWeaponPosition;
    private int damage;
    private float knockBack;

    private Player player;
    private WeaponSO weaponSO;
    
    public float AttackRange { get; private set; }
    public float AttackRate { get; private set; }
    
    public float Force { get; private set; }
    public float ForceTransitionTime { get; private set; }
    
    public float Dealing_Start_TransitionTime { get; private set; }
    public float Dealing_End_TransitionTime { get; private set; }
    

    public void Init(Player _player, WeaponSO _weaponSO)
    {
        player = _player;
        SetWeapon(_weaponSO);
    }
    
    public void SetWeapon(WeaponSO _weaponSO)
    {
        weaponSO = _weaponSO;
        SetDamage();
        this.knockBack = weaponSO.knockBackForce;
        this.AttackRange = weaponSO.attackRange;
        this.Force = weaponSO.force;
        this.ForceTransitionTime = weaponSO.forceTransitionTime;
        this.Dealing_Start_TransitionTime = weaponSO.dealing_Start_TransitionTime;
        this.Dealing_End_TransitionTime = weaponSO.dealing_End_TransitionTime;
    }

    public void SetDamage()
    {
        this.damage =weaponSO.power + player.PlayerCondition.Power;
        this.AttackRate = weaponSO.attackRate + player.PlayerCondition.AttackRate;
    }
    
    public void Fire()
    {
        Debug.Log("Fire");
        // TODO: Projectile Init 
    }

}