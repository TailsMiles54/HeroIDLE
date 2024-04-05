using System;
using System.Linq;
using BlackTailsUnityTools.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelView : MonoBehaviourPrefab
{
    [field: SerializeField] public TMP_Text Title;
    [field: SerializeField] public TMP_Text Info;
    [field: SerializeField] public TMP_Text Content;
    [field: SerializeField] public Image Image;
    [SerializeField] private Button _button;

    private UpgradeSetting.UpgradeType _upgradeType;
    private PlayerController.UpgradeLevel CurrentPlayerUpgrade => PlayerController.Instance.Upgrades.First(x => x.Type == _upgradeType);
    
    public void Setup(UpgradeSetting upgradeSetting)
    {
        UpdatePanel(upgradeSetting);
        _button.onClick.AddListener(() => Upgrade(upgradeSetting));
        SaveManager.Instance.Loaded += () => UpdatePanel(SettingsProvider.Get<UpgradesSettings>().Upgrades.First(x => x.Type == _upgradeType));
        PlayerController.Instance.Upgraded += () => UpdatePanel(SettingsProvider.Get<UpgradesSettings>().Upgrades.First(x => x.Type == _upgradeType));
    }

    private void UpdatePanel(UpgradeSetting upgradeSetting, bool withAnim = false)
    {
        _upgradeType = upgradeSetting.Type; 
        Title.text = upgradeSetting.Name;
        Info.text = $"{CurrentPlayerUpgrade.Level}/{upgradeSetting.Value.Count}" +
                    $"\n{upgradeSetting.Value[CurrentPlayerUpgrade.Level].Value} -> <color=\"green\">{upgradeSetting.Value[CurrentPlayerUpgrade.Level+1].Value}<color=#005500>";
        Content.text = $"Цена: {upgradeSetting.Value[CurrentPlayerUpgrade.Level+1].Cost}";
        Image.sprite = upgradeSetting.Icon;
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