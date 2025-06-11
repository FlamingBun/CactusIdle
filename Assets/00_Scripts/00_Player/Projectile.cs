using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private PlayerWeaponSO weaponSO;
    private float power;
    private float range;
    private Vector3 direction;
    
    private Action<GameObject> returnToPool;
    
    public void Initialize(PlayerWeaponSO _weaponSO, float _power, Vector3 _direction)
    {
        weaponSO = _weaponSO;
        power = _power;
        range = weaponSO.attackRange + 1f;
        direction = _direction;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        Vector3 distanceVector = direction * weaponSO.projectileSpeed * Time.deltaTime;

        transform.position += distanceVector;

        // Disable after max range reached
        range -= distanceVector.magnitude;

        if (range < 0f)
        {
            DisableProjectile();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (layerMask.value == (layerMask.value | (1 << collision.gameObject.layer)))
        {
            collision.GetComponent<Enemy>()?.Condition.TakeDamage(power+Random.Range(0f, power / 10f));
        }

        // TODO: 피격 사운드 재생
        DisableProjectile();
    }
    
    private void DisableProjectile()
    {
        ObjectPoolManager.Instance.ReturnObject(weaponSO.projectile, this.gameObject);
        //this.gameObject.SetActive(false);
    }
}