using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "ScriptableObject/Player/PlayerStatSO", order = 1)]
public class PlayerStatSO : ScriptableObject
{
        public int level;
        public int HP;
        public int MP;
        public float exp;
        
        public int power;
        public float attackRate;

        public float moveSpeed;
        public float rotationDamping;

        public float hitRecoveryTime;
}
