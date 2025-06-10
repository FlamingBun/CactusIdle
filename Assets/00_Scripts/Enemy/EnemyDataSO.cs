
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO_", menuName = "Enemy/EnemyDataSO", order = 0)]
public class EnemyDataSO : ScriptableObject
{
    public GameObject prefab;
    public EnemyStatSO enemyStatSO;
}