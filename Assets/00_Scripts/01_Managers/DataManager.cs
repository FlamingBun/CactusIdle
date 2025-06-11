
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItmeDataPair
{
    public int key;
    public ItemSO itemSO;
}

[Serializable]
public class ItemDataWrapper
{
    public List<ItmeDataPair> itemList = new();
}

public class DataManager : MonoBehaviour
{
    public PlayerDataSO PlayerData { get; private set; }
    public StageDatabaseSO StageDatabaseSO{ get; private set; }
    public EnemyDatabaseSO EnemyDatabaseSO{ get; private set; }
    public ItemDatabaseSO ItemDatabaseSO{ get; private set; }

    #region Gold
    private const string GoldKey = "Gold";
    public int Gold { get; private set; }
    public event Action<int> OnGoldChange;
    #endregion Gold
    
    
    #region Items
    private const string HasItemKey = "HasItems";
    private const string LookItemKey = "LookItem";
    private const string RideItemKey = "RideItem";
    #endregion Items


    
    public void Init()
    {
        PlayerData = Resources.Load<PlayerDataSO>("PlayerDataSO");
        EnemyDatabaseSO = Resources.Load<EnemyDatabaseSO>("EnemyDatabaseSO");
        StageDatabaseSO = Resources.Load<StageDatabaseSO>("StageDatabaseSO");
        ItemDatabaseSO = Resources.Load<ItemDatabaseSO>("ItemDatabaseSO");
        GetGold();
    }
    
    
    private void GetGold()
    {
        Gold = PlayerPrefs.GetInt(GoldKey, 0);
    }

    public StageDataSO GetStageInfo(int stageLevel)
    {
        return StageDatabaseSO.stageDatas[stageLevel];
    }
    
    public void EarnGold(int addGold)
    {
        Gold += addGold;
        
        OnGoldChange?.Invoke(Gold);
        PlayerPrefs.SetInt(GoldKey, Gold);
    }

    public bool SpendGold(int spendGold)
    {
        if (Gold >= spendGold)
        {
            Gold -= spendGold;
            OnGoldChange?.Invoke(Gold);
            PlayerPrefs.SetInt(GoldKey, Gold);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveHasItems(Dictionary<int, ItemSO> hasItems)
    {
        ItemDataWrapper wrapper = new();

        foreach (var pair in hasItems)
        {
            wrapper.itemList.Add(new ItmeDataPair { key = pair.Key, itemSO = pair.Value });
        }

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(HasItemKey, json);
        PlayerPrefs.Save();
    }

    public Dictionary<int, ItemSO> LoadHasItems()
    {
        Dictionary<int, ItemSO> hasItems = new Dictionary<int, ItemSO>();

        string json = PlayerPrefs.GetString(HasItemKey);

        if (!string.IsNullOrEmpty(json))
        {
            ItemDataWrapper wrapper = JsonUtility.FromJson<ItemDataWrapper>(json);

            foreach (var pair in wrapper.itemList)
            {
                hasItems[pair.key] = pair.itemSO;
            }
        }
        else
        {
            GameManager.Instance.ItemManager.AddItem(1);
        }

        return hasItems;
    }

    public void SavePlayerData(int lookId, int rideId)
    {
        PlayerPrefs.SetInt(LookItemKey, lookId);
        PlayerPrefs.SetInt(RideItemKey, rideId);
    }

}