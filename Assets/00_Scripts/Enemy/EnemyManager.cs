
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Player player;

    public Enemy enemy;
    public void Init()
    {
        player = GameManager.Instance.Player;
    }


    public Enemy GetNearestEnemyFromPlayer()
    {
        return enemy;
    }
}