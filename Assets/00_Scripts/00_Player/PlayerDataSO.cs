using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "ScriptableObject/Player/PlayerDataSO", order = 0)] 
public class PlayerDataSO : ScriptableObject 
{
    public GameObject player;
    public PlayerStatSO playerStat;
    public PlayerWeaponSO weaponSO;
}
