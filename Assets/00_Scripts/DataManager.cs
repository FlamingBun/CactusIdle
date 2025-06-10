
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerDataSO PlayerData { get; private set; }

    public void Init()
    {
        PlayerData = Resources.Load<PlayerDataSO>("PlayerDataSO");    
    }
}