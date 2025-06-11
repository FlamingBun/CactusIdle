using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponSO_", menuName = "ScriptableObject/Player/PlayerWeaponSO", order = 2)] 
public class PlayerWeaponSO:WeaponSO
{
    public float projectileSpeed;
    public GameObject projectile;
}
