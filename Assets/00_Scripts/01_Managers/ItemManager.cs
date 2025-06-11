using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private DataManager dataManager;
    private ItemDatabaseSO itemDatabase;
    
    public List<ItemSO> Items { get { return itemDatabase.items; } }
    private Dictionary<int, ItemSO> itemDatas;

    [SerializeField] private Dictionary<int, ItemSO> hasItems;
    
    public void Init()
    {
        hasItems = new Dictionary<int, ItemSO>();
        itemDatas = new Dictionary<int, ItemSO>();

        dataManager = GameManager.Instance.DataManager;

        itemDatabase = dataManager.ItemDatabaseSO;
        
        foreach (var item in itemDatabase.items)
        {
            itemDatas.Add(item.itemId, item);
        }
        
        hasItems = dataManager.LoadHasItems();
    }

    public bool HasItem(int itemId)
    {
        if (hasItems.ContainsKey(itemId))
            return true;
        else
            return false;
    }

    public void AddItem(int itemId)
    {
        if (HasItem(itemId))
        {
            Logger.Log("이미 구매한 아이템입니다.");
            return;
        }

        ItemSO item = itemDatas[itemId];

        hasItems.Add(item.itemId, item);
        dataManager.SpendGold(item.price);
        dataManager.SaveHasItems(hasItems);
    }

    public ItemSO GetItem(int itemId)
    {
        return itemDatas[itemId];
    }

    public void UseItem(ConsumableItemSO consumableItemSO)
    {
        hasItems.Remove(consumableItemSO.itemId);
    }
}