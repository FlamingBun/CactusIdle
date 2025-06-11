using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDatabaseSO",menuName = "ScriptableObject/Stage/StageDatabase")]
public class StageDatabaseSO:ScriptableObject
{
    public List<StageDataSO> stageDatas;
}
