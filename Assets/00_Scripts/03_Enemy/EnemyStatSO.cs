
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatSO_", menuName = "ScriptableObject/Enemy/EnemyStatSO", order = 1)]
public class EnemyStatSO : ScriptableObject
{
    public int HP;
    public float exp;
}