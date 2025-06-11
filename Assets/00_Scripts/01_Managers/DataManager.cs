
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerDataSO PlayerData { get; private set; }
    public StageDatabaseSO StageDatabaseSO{ get; private set; }
    public EnemyDatabaseSO EnemyDatabaseSO{ get; private set; }

    public void Init()
    {
        PlayerData = Resources.Load<PlayerDataSO>("PlayerDataSO");
        EnemyDatabaseSO = Resources.Load<EnemyDatabaseSO>("EnemyDatabaseSO");
        StageDatabaseSO = Resources.Load<StageDatabaseSO>("StageDatabaseSO");
    }

    public StageDataSO GetStageInfo(int stageLevel)
    {
        return StageDatabaseSO.stageDatas[stageLevel];
    }
}