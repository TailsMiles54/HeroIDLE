using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelView : MonoBehaviourPrefab
{
    [field: SerializeField] public TMP_Text Title;
    [field: SerializeField] public TMP_Text Info;
    [field: SerializeField] public TMP_Text Content;
    [SerializeField] private Button _button;

    private UpgradeSetting.UpgradeType _upgradeType;
    private PlayerController.UpgradeLevel CurrentPlayerUpgrade => PlayerController.Instance.Upgrades.First(x => x.Type == _upgradeType);
    
    public void Setup(UpgradeSetting upgradeSetting)
    {
        UpdatePanel(upgradeSetting);
        _button.onClick.AddListener(() => Upgrade(upgradeSetting));
    }

    private void UpdatePanel(UpgradeSetting upgradeSetting)
    {
        _upgradeType = upgradeSetting.Type; 
        Title.text = upgradeSetting.Type.ToString();
        Info.text = $"{CurrentPlayerUpgrade.Level}/{upgradeSetting.Value.Count}" +
                    $"\n{upgradeSetting.Value[CurrentPlayerUpgrade.Level].Value} --> <color=\"green\">{upgradeSetting.Value[CurrentPlayerUpgrade.Level+1].Value}<color=#005500>";
        Content.text = $"Price: {upgradeSetting.Value[CurrentPlayerUpgrade.Level+1].Cost}";
    }

    private void Upgrade(UpgradeSetting upgradeSetting)
    {
        if(PlayerController.Instance.TryPurchase(upgradeSetting.Value[CurrentPlayerUpgrade.Level+1].Cost))
        {
            PlayerController.Instance.Upgrade(upgradeSetting.Type);
            UpdatePanel(upgradeSetting);
        }
    }
}

public class MonoBehaviourPrefab : MonoBehaviour
{
    
}