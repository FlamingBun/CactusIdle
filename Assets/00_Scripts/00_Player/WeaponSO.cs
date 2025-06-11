using UnityEngine;

public class WeaponSO: ItemSO
{
    [Space(10)]
    [Header("Attack")]
    public int power;
    public float attackRate;
    public float attackRange;
    public float knockBackForce;
    
    [Space(10)]
    [Header("Force")]
    [Range(-10f, 10f)] public float force;
    [Range(0f, 3f)] public float forceTransitionTime;
    [Range(0f, 1f)] public float dealing_Start_TransitionTime;
    [Range(0f, 1f)] public float dealing_End_TransitionTime;
    
    public GameObject attackEffect;
}
