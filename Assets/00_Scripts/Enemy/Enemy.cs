using UnityEngine;

[RequireComponent(typeof(EnemyCondition))]
public class Enemy:MonoBehaviour
{
    public EnemyCondition Condition {get; private set; }

    private void Awake()
    {
        Condition = GetComponent<EnemyCondition>();
    }
}
