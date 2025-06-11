using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
    protected override UIKey uiKey { get; } =  UIKey.ShopUI;

    private ItemManager itemManager;
    
    [SerializeField] private Button exitButton;
    [SerializeField] private Button purchasedButton;
    [SerializeField] private Button purchaseButton;
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
        }
    }

    public void SelectSlot(int _itemId)
    {
        selectedItemId = _itemId;

        if (itemManager.HasItem(_itemId))
        {
            purchasedButton.gameObject.SetActive(true);
            purchaseButton.gameObject.SetActive(false);
        }
        else
        {
            purchasedButton.gameObject.SetActive(false);
            purchaseButton.gameObject.SetActive(true);

            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(OnClickPurchaseButton);
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

    private void OnClickPurchaseButton()
    {
        itemManager.AddItem(selectedItemId);
        SelectSlot(selectedItemId);
    }
}
