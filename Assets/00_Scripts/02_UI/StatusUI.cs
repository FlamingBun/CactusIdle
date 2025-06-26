using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    private Player player;
    private DataManager dataManager;

    private WaitForSeconds ws=new WaitForSeconds(0.2f);
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI attackRateText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI goldText;

    private void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        while (player == null||dataManager == null)
        {
            player = GameManager.Instance.Player;
            dataManager = GameManager.Instance.DataManager;
            yield return ws;
        }
        
        stageText.text = $"스테이지 : {GameManager.Instance.StageManager.CurrentStageLevel+1}";
        expText.text = $"경험치 : {player.Condition.Exp}";
        levelText.text = $"레벨 : {player.Condition.Level}";
        powerText.text = $"공격력 : {player.Weapon.power}";
        attackRateText.text = $"공격 속도 : {player.Weapon.AttackRate:F1}";
        speedText.text = $"이동 속도: {player.Condition.CurrentMoveSpeed:F1}";
        goldText.text = $"골드: {dataManager.Gold}";

        player.Condition.OnLevelChange += LevelChange;
        player.Condition.OnExpChange += ExpChange;
        player.Weapon.OnTotalPowerChanged += TotalPowerChanged;
        GameManager.Instance.StageManager.OnChageStage += StageChange;
        dataManager.OnGoldChange += GoldChange;
        player.Condition.OnMoveSpeedChange += ChangeMoveSpeed;
    }
    
    private void OnDisable()
    {
        if (player == null || dataManager == null) return;
        
        dataManager.OnGoldChange -= GoldChange;
        player.Condition.OnMoveSpeedChange -= ChangeMoveSpeed;
    }

    private void StageChange(int stage)
    {
        stageText.text = $"스테이지 : {stage+1}";
    }

    private void TotalPowerChanged(float power, float attackRate)
    {
        powerText.text = $"공격력 : {power}";
        attackRateText.text = $"공격 속도 : {attackRate:F1}";
    }

    private void ExpChange(float obj)
    {
        expText.text = $"경험치 : {player.Condition.Exp}";
    }

    private void LevelChange(float obj)
    {
        levelText.text = $"레벨 : {player.Condition.Level}";
    }

    private void GoldChange(int gold)
    {
        goldText.text = $"골드: {gold}";
    }

    private void ChangeMoveSpeed(float speed, float rotation)
    {
        if (speed == 0f) return;
        speedText.text = $"이동 속도: {speed:F1}";
    }
}
