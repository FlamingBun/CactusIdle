using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button shopButton;
    private void Awake()
    {
        shopButton.onClick.AddListener(OpenShopUI);
    }

    private void OpenShopUI()
    {
        GameManager.Instance.UIManager.OpenUI(UIKey.ShopUI);
    }
}
