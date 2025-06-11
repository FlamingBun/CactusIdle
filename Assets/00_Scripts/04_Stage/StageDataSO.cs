using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO_", menuName = "ScriptableObject/Stage/StageDataSO")]
public class StageDataSO:ScriptableObject
{
    public int stageLevel;
    public Vector3 minBound;
    public Vector3 maxBound;
}
