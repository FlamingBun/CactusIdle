using UnityEngine;
using UnityEngine.UI;

public class StageProgressBar : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0f;
    }

    void Start()
    {
        GameManager.Instance.EnemyManager.OnEnemyCountChange += OnEnemyCountChange;
    }

    public void OnEnemyCountChange(int current, int total)
    {
        slider.value = 1f-(float)((float)current / (float)total);
    }
}
