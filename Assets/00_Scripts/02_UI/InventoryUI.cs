using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseUI
{
    protected override UIKey uiKey { get; } =  UIKey.InventoryUI;

    private ItemManager itemManager;
    
    [SerializeField] private Button exitButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button useButton;
    [SerializeField] private Transform content;
    [SerializeField] GameObject slotPrefab;
    
    private List<ItemSO> hasItems;
    private List<SlotButton> slotList = new();
    
    WaitForSeconds ws = new WaitForSeconds(0.2f);

    private int selectedItemId;

    protected override void Initialize()
    {
        base.Initialize();
        itemManager =GameManager.Instance.ItemManager;
        exitButton.onClick.AddListener(()=>GameManager.Instance.UIManager.CloseUI());
            
        hasItems = itemManager.hasItemList;
    }


    private void OnEnable()
    {
        if (itemManager == null) return;
        SetSlot();
        SelectSlot(1);
    }

    private void OnDisable()
    {
        if(ObjectPoolManager.Instance == null) return;
        
        foreach (var slot in slotList)
        {
            ObjectPoolManager.Instance.ReturnObject(slotPrefab,slot.gameObject);
        }
    }

    private void SetSlot()
    {
        foreach (var item in hasItems)
        {
            GameObject slot = ObjectPoolManager.Instance.GetObject(slotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(content, false);
                
            SlotButton slotButton = slot.GetComponent<SlotButton>();
            slotButton.SetItemInfo(item);
            slotButton.Initialize(this);
                
            slotList.Add(slotButton);
            slot.SetActive(true);
        }
    }

    public void SelectSlot(int _itemId)
    {
        selectedItemId = _itemId;

        if (itemManager.HasItem(_itemId))
        {
            if (itemManager.GetItem(_itemId).itemType == ItemType.Consumable)
            {
                equipButton.gameObject.SetActive(false);
                useButton.gameObject.SetActive(true);
                
                useButton.onClick.RemoveAllListeners();
                useButton.onClick.AddListener(OnClickUseButton);
            }
            else
            {
                equipButton.gameObject.SetActive(true);
                useButton.gameObject.SetActive(false);

                equipButton.onClick.RemoveAllListeners();
                equipButton.onClick.AddListener(OnClickEquipButton);       
            }
        }

        SetSelectedFrame();
    }

    private void SetSelectedFrame()
    {
        foreach (var slot in slotList)
        {
            slot.SetBackgroundColor(itemManager.HasItem(slot.itemId));

            if (slot.itemId == selectedItemId)
                slot.SetFrameActive(true);
            else
                slot.SetFrameActive(false);
        }
    }

    private void OnClickEquipButton()
    {
        PlayerWeaponSO weaponSO = itemManager.GetItem(selectedItemId) as PlayerWeaponSO;
        Logger.Log("selected Item Id : " + selectedItemId);
        Logger.Log("selected Item name : " + weaponSO.name);
        GameManager.Instance.Player.EquipItem(weaponSO);
    }
    
    private void OnClickUseButton()
    {
        ConsumableItemSO consumableItemSO = itemManager.GetItem(selectedItemId) as ConsumableItemSO;
        Logger.Log("selected Item Id : " + selectedItemId);
        Logger.Log("selected Item name : " + consumableItemSO.name);
        itemManager.UseItem(consumableItemSO);
        GameManager.Instance.Player.UseItem(consumableItemSO);
    }
    
}
