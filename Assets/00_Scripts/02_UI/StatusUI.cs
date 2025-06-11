using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    private Player player;
    private DataManager dataManager;

    private WaitForSeconds ws=new WaitForSeconds(1f); 
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI pointText;

    private void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        while (player == null||dataManager == null)
        {
            yield return ws;
        }
        
        player = GameManager.Instance.Player;
        dataManager = GameManager.Instance.DataManager;
        speedText.text = $"이동 속도: {player.Condition.CurrentMoveSpeed}";
        pointText.text = $"골드: {dataManager.Gold}";
    }

    private void OnEnable()
    {
        if (player == null || dataManager == null) return;
        
        dataManager.OnGoldChange += GoldChange;
        player.Condition.OnMoveSpeedChange += ChangeMoveSpeed;
    }

    private void OnDisable()
    {
        if (player == null || dataManager == null) return;
        
        dataManager.OnGoldChange -= GoldChange;
        player.Condition.OnMoveSpeedChange -= ChangeMoveSpeed;
    }

    private void GoldChange(int gold)
    {
        pointText.text = $"골드: {gold}";
    }

    private void ChangeMoveSpeed(float speed, float rotation)
    {
        speedText.text = $"이동 속도: {speed:F1}";
    }
}
