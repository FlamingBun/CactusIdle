using System;
using System.Collections;
using UnityEngine;

public class EnemyCondition:MonoBehaviour,IDamageable
{
    private EnemyStatSO enemyStatSO;
    #region Stat
    private float exp;
    private float currentHP;
    #endregion Stat
    
    #region Events
    public event Action<float, float> OnHPChange;
    #endregion Events
    
    
    #region Hit
    private bool isHit = false;
    public bool IsDead { get; private set; } =  false;
    
    private float hitRecoveryTime;
    private float currentHitRecoveryTime;
    #endregion Hit
    
    public void Init(EnemyStatSO _enemyStatSO)
    {
        enemyStatSO = _enemyStatSO;
        exp = _enemyStatSO.exp;
        
        currentHP = _enemyStatSO.HP;
        
        isHit = false;
        IsDead = false;
    }
    
    public void TakeDamage(float damage)
    {
        if (IsDead||isHit) return;
        
        currentHP -= damage;
        OnHPChange?.Invoke(currentHP, enemyStatSO.HP);
        if (currentHP <= 0)
        { 
            IsDead = true;
            // TODO : Object Pool로
            return;
        }
        
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        isHit = true;
        //ChangeToHitState();
        currentHitRecoveryTime = hitRecoveryTime;
        while (currentHitRecoveryTime > 0)
        {
            currentHitRecoveryTime -= Time.deltaTime;
            yield return null;
        }
        //ChangeToIdleState();
        
        isHit = false;
    }
}
