using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private DataManager dataManager;
    private ItemDatabaseSO itemDatabase;
    
    public List<ItemSO> Items { get { return itemDatabase.items; } }
    private Dictionary<int, ItemSO> itemDatas;

    private Dictionary<int, ItemSO> hasItems;
    public List<ItemSO> hasItemList;
    
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
        if (hasItems.Count == 0)
        {
            AddItem(1);
        }

        foreach (var item in hasItems)
        {
            hasItemList.Add(item.Value);   
        }
        
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
        
        if (!dataManager.SpendGold(item.price)&& itemId != 1)
        {
            Logger.Log("골드가 부족합니다.");
            return;
        }

        hasItems.Add(item.itemId, item);
        hasItemList.Add(item);
        
        dataManager.SaveHasItems(hasItems);
    }

    public ItemSO GetItem(int itemId)
    {
        return itemDatas[itemId];
    }

    public void UseItem(ConsumableItemSO consumableItemSO)
    {
        hasItems.Remove(consumableItemSO.itemId);
        hasItemList.Remove(consumableItemSO);
    }
}