using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    [SerializeField] private Button slotButton;


    [SerializeField] private Color normalColor;
    [SerializeField] private Color hasColor;

    [SerializeField] private Image background;
    [SerializeField] private Image image;
    [SerializeField] private Image selectedFrame;
    [SerializeField] private TextMeshProUGUI itemName;

    [HideInInspector] public int itemId;

    bool isLookSlot = false;

    private ItemSO itemSO;

    ShopUI shopUI;
    InventoryUI inventoryUI;

    public void SetItemInfo(ItemSO _itemSO)
    {
        itemSO = _itemSO;
        itemId = itemSO.itemId;

        image.sprite = itemSO.sprite;
        itemName.text = itemSO.name;
    }

    public void Initialize(ShopUI _shopUI)
    {
        shopUI = shopUI;
        slotButton.onClick.AddListener(OnClickItemSlot);
        isLookSlot = true;
    }

    public void Initialize(InventoryUI _inventoryUI)
    {
        inventoryUI = _inventoryUI;
        slotButton.onClick.AddListener(OnClickItemSlot);
        isLookSlot = false;
    }

    private void OnClickItemSlot()
    {
        if (isLookSlot)
            shopUI.SelectSlot(itemId);
        else
            inventoryUI.SelectSlot(itemId);
    }

    public void SetFrameActive(bool isSelected)
    {
        selectedFrame.gameObject.SetActive(isSelected);
    }

    public void SetBackgroundColor(bool hasItem)
    {
        if (hasItem)
            background.color = hasColor;
        else
            background.color = normalColor;
    }
}
