using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Stack<UIKey> uiStack = new();
    private Dictionary<UIKey, BaseUI> uiDictionary = new();
    public Dictionary<UIKey, BaseUI> UIDictionary { get => uiDictionary; }
    public Stack<UIKey> UIStack { get => uiStack; }

    //public Action OnClose;
    
    public void Init()
    {
        
    }

    public void SetUI(UIKey uiKey, BaseUI ui)
    {
        uiDictionary.Add(uiKey, ui);
    }

    public void OpenUI(UIKey uiKey)
    {
        if (uiStack.Count > 0)
        {
            CloseUI();
        }

        uiStack.Push(uiKey);
        uiDictionary[uiKey].SetUIActive(true);
    }

    
    // 현재 UI 비활성화
    public void CloseUI()
    {
        if (uiStack.Count == 0) return;
        
        //OnClose?.Invoke();
        uiDictionary[uiStack.Peek()].SetUIActive(false);
        uiStack.Pop();
    }
    
    // 현재 UI 비활성화 후 다음 UI 활성화
    public void ChangeUI(UIKey uiKey)
    {
        CloseUI();
        OpenUI(uiKey);
    }
}