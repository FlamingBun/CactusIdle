using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Button inventoryButton;
    private void Awake()
    {
        inventoryButton.onClick.AddListener(OpenInventoryUI);
    }

    private void OpenInventoryUI()
    {
        GameManager.Instance.UIManager.OpenUI(UIKey.InventoryUI);
    }
}
