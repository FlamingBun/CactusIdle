using System.Collections;
using System.Collections.Generic;
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

    private List<ItemSO> items;

    private List<SlotButton> slotList;

    private int selectedItemId;

    protected override void Initialize()
    {
        base.Initialize();
        itemManager =GameManager.Instance.ItemManager;
        exitButton.onClick.AddListener(()=>GameManager.Instance.UIManager.CloseUI());

        slotList = new List<SlotButton>();

        items = itemManager.Items;
        MakeSlot();
    }

    private void OnEnable()
    {
        if (itemManager == null) return;
        SelectSlot(1);
    }

    private void MakeSlot()
    {
        foreach (var item in items)
        {
            if (item.itemType == ItemType.Weapon)
            {
                GameObject slot = Instantiate(slotPrefab, content);
                SlotButton slotButton = slot.GetComponent<SlotButton>();
                slotButton.SetItemInfo(item);
                slotButton.Initialize(this);
                slotList.Add(slotButton);
            }
            else
            {
                GameObject slot = Instantiate(slotPrefab, content);
                SlotButton slotButton = slot.GetComponent<SlotButton>();
                slotButton.SetItemInfo(item);
                slotButton.Initialize(this);
                slotList.Add(slotButton);
            }
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
