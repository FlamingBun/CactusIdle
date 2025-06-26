using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private DataManager dataManager;
    
    public int PreviousStageLevel { get; private set; } = 0;
    public int CurrentStageLevel { get; private set; } = 0;
    
    public bool IsBoss { get; set; } = true;
    
    public event Action<int> OnChageStage;

    public void Init()
    {
        dataManager= GameManager.Instance.DataManager;
    }

    public void StartStage(int stageLevel)
    {
        PreviousStageLevel = CurrentStageLevel;
        CurrentStageLevel = stageLevel;
        OnChageStage?.Invoke(CurrentStageLevel);
        GameManager.Instance.EnemyManager.SpawnEnemy(dataManager.GetStageInfo(stageLevel), IsBoss);
    }
}