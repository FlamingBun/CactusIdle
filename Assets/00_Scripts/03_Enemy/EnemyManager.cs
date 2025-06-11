
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Player player;
    private StageManager stageManager;
    
    private EnemyDatabaseSO enemyDatabaseSO;
    [SerializeField]private EnemySpawner enemySpawner;

    private StageDataSO currentStageData;
    private int currentStageLevel;
    private int currentEnemyCount;
    private bool isBoss = false;

    private List<Enemy> spawnedEnemies = new();
    
    public void Init()
    {
        player = GameManager.Instance.Player;
        stageManager = GameManager.Instance.StageManager;
        enemyDatabaseSO= GameManager.Instance.DataManager.EnemyDatabaseSO;
    }

    public void SpawnEnemy(StageDataSO stageDataSO, bool _isBoss)
    {
        spawnedEnemies.Clear();
        
        currentStageData = stageDataSO;
        currentStageLevel = stageDataSO.stageLevel;
        currentEnemyCount = enemyDatabaseSO.stageEnemies[ currentStageLevel].spawnCount;

        if (currentStageLevel != stageManager.PreviousStageLevel)
        {
            ObjectPoolManager.Instance.DestroyObjects(enemyDatabaseSO.stageEnemies[stageManager.PreviousStageLevel].enemyDataSO.prefab);
        }

        for (int i = 0; i < currentEnemyCount; i++)
        {
            spawnedEnemies.Add(enemySpawner.SpawnEnemy(enemyDatabaseSO.stageEnemies[currentStageLevel].enemyDataSO, stageDataSO.minBound, stageDataSO.maxBound));   
        }
        
        isBoss = _isBoss;
        if (isBoss) ++currentEnemyCount;
    }

    public Enemy GetNearestEnemyFromPlayer()
    {
        Enemy nearestEnemy =null;
        float minDistance = float.MaxValue;
        int count = spawnedEnemies.Count;
        for (int i = 0; i < count; i++)
        {
            if (spawnedEnemies[i] != null&& !spawnedEnemies[i].Condition.IsDead)
            {
                if (player == null)
                {
                    player = GameManager.Instance.Player;
                }

                float distance = Vector3.Distance(spawnedEnemies[i].transform.position, player.transform.position);
                if (distance < minDistance)
                {
                    nearestEnemy =  spawnedEnemies[i];
                    minDistance = distance;
                }
            }
        }

        return nearestEnemy;
    }

    public void DieEnemy()
    {
        --currentEnemyCount;
        if (isBoss)
        {
            if (currentEnemyCount == 1)
            {
                enemySpawner.SpawnEnemy(enemyDatabaseSO.stageEnemies[currentStageLevel - 1].bossDataSO, currentStageData.minBound, currentStageData.maxBound);
            }

            if (currentEnemyCount <= 0)
            {
                GameManager.Instance.ClearAndNextStage();
            }
        }
        else
        {
            if (currentEnemyCount <= 0)
            {
                GameManager.Instance.ClearStage();
            }
        }
    }
}