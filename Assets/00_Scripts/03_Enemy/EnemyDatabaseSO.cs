using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabaseSO", menuName = "ScriptableObject/Enemy/EnemyDatabase")]
public class EnemyDatabaseSO : ScriptableObject
{
    public List<StageEnemy> stageEnemies;
}

[Serializable]
public class StageEnemy
{
    public int spawnCount;
    public EnemyDataSO enemyDataSO;
    public EnemyDataSO bossDataSO;
}