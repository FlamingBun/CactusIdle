using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private LayerMask obstacleMask;
    
    public Enemy SpawnEnemy(EnemyDataSO enemyDataSO, Vector3 min, Vector3 max)
    {
        Enemy spawnEnemy = ObjectPoolManager.Instance.GetObject(enemyDataSO.prefab, GetRandomSpawnPosition(min, max), Quaternion.identity).GetComponent<Enemy>();
        spawnEnemy.Initialize(enemyDataSO);
        spawnEnemy.gameObject.SetActive(true);
        
        return spawnEnemy;
        
    }

    private Vector3 GetRandomSpawnPosition(Vector3 min, Vector3 max)
    {
        Vector3 randomPos;
        for (int i = 0; i < Settings.maxEnemySpawnAttempts; i++)
        {
            randomPos = new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
            );
            
            if (!Physics.CheckSphere(randomPos, Settings.checkEnemySpawnRadius, obstacleMask))
            {
                return randomPos;
            }
        }
        
        return min;
    }

}