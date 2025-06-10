using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Player/PlayerDataSO", order = 0)] 
public class PlayerDataSO : ScriptableObject 
{
    public GameObject player;
    public PlayerStatSO playerStat;
    public PlayerWeaponSO weaponSO;
}
