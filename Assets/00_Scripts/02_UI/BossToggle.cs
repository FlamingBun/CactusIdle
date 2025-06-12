using UnityEngine;
using UnityEngine.UI;

public class BossToggle : MonoBehaviour
{
   private Toggle toggle;

   private void Awake()
   {
      toggle = GetComponent<Toggle>();
   }

   private void Start()
   {
      toggle.onValueChanged.AddListener(OnToggleValueChanged);
      toggle.isOn = true;
   }

   private void OnDestroy()
   {
      toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
   }

   private void OnToggleValueChanged(bool isOn)
   {
      GameManager.Instance.StageManager.IsBoss = isOn;
   }
}
