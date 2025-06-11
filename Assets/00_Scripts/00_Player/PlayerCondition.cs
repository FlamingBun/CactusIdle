using System;
using System.Collections;
using UnityEngine;

public class PlayerCondition:MonoBehaviour, IDamageable
{
    
    private PlayerStatSO playerStat;
    #region Stat
    private int level;
    private float exp;
    private float currentHP;
    private float currentMP;

    public int Level { get => level; }
    public float Exp { get => exp; }
    public int Power { get; private set; }
    public float AttackRate { get; private set; }

    #endregion Stat
    
    #region Move
    private float previousMoveSpeedMultiplier;
    private float currentMoveSpeedMultiplier;
    
    public float CurrentMoveSpeed { get; private set; }
    public float CurrentRotationDamping{ get; private set; }
    #endregion Move
    
    #region Hit
    private bool isHit = false;
    private bool isDead = false;
    
    private float hitRecoveryTime;
    private float currentHitRecoveryTime;
    #endregion Hit
    
    #region Events
    public event Action<float, float> OnHPChange;
    public event Action<float, float> OnMPChange;
    public event Action<float,float> OnMoveSpeedChange;
    public event Action<float> OnLevelChange;
    public event Action<float> OnExpChange;
    public event Action<float> OnPowerChange;
    public event Action<float> OnAttackRateChange;
    #endregion Events

    private Coroutine changeMoveSpeedRoutine;
    
    public void Init(PlayerStatSO _playerStat)
    {
        playerStat = _playerStat;
        
        level = playerStat.level;
        exp = playerStat.exp;
        
        Power =  playerStat.power;
        AttackRate = playerStat.attackRate;
        
        currentHP = playerStat.HP;
        currentMP = playerStat.MP;
        
        hitRecoveryTime =  playerStat.hitRecoveryTime;
        currentHitRecoveryTime = playerStat.hitRecoveryTime;
        
        CurrentMoveSpeed = playerStat.moveSpeed;
        CurrentRotationDamping = playerStat.rotationDamping;
    }

    public void TakeDamage(float damage)
    {
        if (isDead||isHit) return;
        
        currentHP -= damage;
        OnHPChange?.Invoke(currentHP, playerStat.HP);

        if (currentHP <= 0)
        { 
            isDead = true;
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

    
    // ==== MoveSpeed ====
    public void ChangeMoveSpeed(float multiplier)
    {
        ChangeMoveSpeedMultiplier(multiplier);
    }

    public void SetPreviousMoveSpeed()
    {
        ChangeMoveSpeedMultiplier(previousMoveSpeedMultiplier);
    }

    public void ChangeMoveSpeed(float multiplier, float time)
    {
        if (changeMoveSpeedRoutine != null)
        {
            StopCoroutine(changeMoveSpeedRoutine);
        }
        changeMoveSpeedRoutine = StartCoroutine(ChangeMoveSpeedRoutine(multiplier, time));
    }

    private IEnumerator ChangeMoveSpeedRoutine(float multiplier, float time)
    {
        ChangeMoveSpeedMultiplier(multiplier);
        yield return new WaitForSeconds(time);
        SetPreviousMoveSpeed();
    }

    private void ChangeMoveSpeedMultiplier(float multiplier)
    {
        previousMoveSpeedMultiplier = currentMoveSpeedMultiplier;
        currentMoveSpeedMultiplier = multiplier;
        OnMoveSpeedChange?.Invoke(CurrentMoveSpeed*currentMoveSpeedMultiplier, CurrentRotationDamping*currentMoveSpeedMultiplier);
    }
    // ==== end MoveSpeed ====

    public void AddEXP(float _exp)
    {
        exp += _exp;
        OnExpChange?.Invoke(playerStat.exp);
        if (exp >= 100)
        {
            int upLevel = (int)(exp / 100);
            level += upLevel;
            exp = exp%100;
            playerStat.level = level;
            playerStat.exp = exp;
            playerStat.power += 1 * upLevel;
            playerStat.attackRate += 0.1f * upLevel;
            playerStat.moveSpeed += 0.1f * upLevel;
            playerStat.rotationDamping += 0.01f * upLevel;
            RefreshStatsAfterLevelUp();
        }
    }
    
    private void RefreshStatsAfterLevelUp()
    {
        // HP/MP를 최대치로 회복
        currentHP = playerStat.HP;
        currentMP = playerStat.MP;
        
        Power = playerStat.power;
        AttackRate = playerStat.attackRate;
        
        // 이동 속도 갱신
        CurrentMoveSpeed = playerStat.moveSpeed;
        CurrentRotationDamping = playerStat.rotationDamping;

        // 이벤트 호출 (UI 갱신용)
        OnHPChange?.Invoke(currentHP, playerStat.HP);
        OnMPChange?.Invoke(currentMP, playerStat.MP);
        OnMoveSpeedChange?.Invoke(CurrentMoveSpeed * currentMoveSpeedMultiplier, CurrentRotationDamping * currentMoveSpeedMultiplier);
        OnLevelChange?.Invoke(playerStat.level);
        OnPowerChange?.Invoke(playerStat.power);
        OnAttackRateChange?.Invoke(playerStat.attackRate);
    }
}
