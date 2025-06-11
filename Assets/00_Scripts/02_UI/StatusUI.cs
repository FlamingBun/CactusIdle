using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    private Player player;
    private DataManager dataManager;

    private WaitForSeconds ws=new WaitForSeconds(1f);
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
        attackRateText.text = $"공격 속도 : {player.Weapon.AttackRate}";
        speedText.text = $"이동 속도: {player.Condition.CurrentMoveSpeed:F1}";
        goldText.text = $"골드: {dataManager.Gold}";

        player.Condition.OnLevelChange += LevelChange;
        player.Condition.OnExpChange += ExpChange;
        player.Condition.OnPowerChange += PowerChange;
        player.Condition.OnAttackRateChange += AttackRateChange;
        dataManager.OnGoldChange += GoldChange;
        player.Condition.OnMoveSpeedChange += ChangeMoveSpeed;
    }

    private void AttackRateChange(float obj)
    {
        attackRateText.text = $"공격 속도 : {player.Weapon.AttackRate}";
    }

    private void PowerChange(float obj)
    {
        powerText.text = $"공격력 : {player.Weapon.power}";
    }

    private void ExpChange(float obj)
    {
        expText.text = $"경험치 : {player.Condition.Exp}";
    }

    private void LevelChange(float obj)
    {
        levelText.text = $"레벨 : {player.Condition.Level}";
    }

    private void OnDisable()
    {
        if (player == null || dataManager == null) return;
        
        dataManager.OnGoldChange -= GoldChange;
        player.Condition.OnMoveSpeedChange -= ChangeMoveSpeed;
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
