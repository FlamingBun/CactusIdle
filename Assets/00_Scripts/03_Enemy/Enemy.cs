using UnityEngine;

[RequireComponent(typeof(EnemyCondition))]
public class Enemy:MonoBehaviour
{
    private EnemyDataSO enemyDataSO;
    public EnemyCondition Condition {get; private set; }
    
    private void Awake()
    {
        Condition = GetComponent<EnemyCondition>();
    }

    private void OnEnable()
    {
        Condition.OnDieEvent += OnDieEvent;
    }

    private void OnDisable()
    {
        Condition.OnDieEvent -= OnDieEvent;
    }

    public void Initialize(EnemyDataSO _enemyDataSO)
    {
        enemyDataSO = _enemyDataSO;
        Condition.Init(enemyDataSO.enemyStatSO);
    }

    public void OnDieEvent()
    {
        GameManager.Instance.EnemyManager.DieEnemy();
        ObjectPoolManager.Instance.ReturnObject(enemyDataSO.prefab, this.gameObject);
    }
}
