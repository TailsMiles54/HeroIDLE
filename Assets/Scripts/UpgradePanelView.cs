using System;
using System.Collections.Generic;
using System.Linq;
using BlackTailsUnityTools.Editor;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelView : RightPanelElement
{
    [field: SerializeField] public TMP_Text Title;
    [field: SerializeField] public TMP_Text Info;
    [field: SerializeField] public TMP_Text Content;
    [field: SerializeField] public Image Image;
    [SerializeField] private Button _button;

    private bool _shakeAnim;
    private bool NotMaxLevel => CurrentPlayerUpgrade().Level < SettingsProvider.Get<UpgradesSettings>().Upgrades.First(x => x.Type == _upgradeType).Value.Count - 1;
    
    private UpgradeSetting.UpgradeType _upgradeType;
    private PlayerController.UpgradeLevel CurrentPlayerUpgrade()
    {
        if(PlayerController.Instance.Upgrades.All(x => x.Type != _upgradeType))
            PlayerController.Instance.Upgrades.Add(new PlayerController.UpgradeLevel() { Type = UpgradeSetting.UpgradeType.ClickDamage, Level = 0 });
            
        return PlayerController.Instance.Upgrades.First(x => x.Type == _upgradeType);
    }
    
    public void Setup(UpgradeSetting upgradeSetting)
    {
        UpdatePanel(upgradeSetting);
        _button.onClick.AddListener(() => Upgrade(upgradeSetting));
        SaveManager.Instance.Loaded += () => UpdatePanel(SettingsProvider.Get<UpgradesSettings>().Upgrades.First(x => x.Type == _upgradeType));
        PlayerController.Instance.Upgraded += (upgradeType) =>
        {
            if(upgradeType == _upgradeType && RightPanelContentController.Instance.CurrentTab == TabType.Upgrades)
                UpdatePanel(SettingsProvider.Get<UpgradesSettings>().Upgrades.First(x => x.Type == _upgradeType), true);
        };
    }

    private void UpdatePanel(UpgradeSetting upgradeSetting, bool withAnim = false)
    {
        _upgradeType = upgradeSetting.Type; 
        Title.text = upgradeSetting.Name;

        var infoText = String.Empty;

        if (NotMaxLevel)
            infoText += $"{CurrentPlayerUpgrade().Level}/{upgradeSetting.Value.Count - 1}" +
                $"\n{upgradeSetting.Value[CurrentPlayerUpgrade().Level].Value} -> <color=#FFA300>{upgradeSetting.Value[CurrentPlayerUpgrade().Level + 1].Value}<color=#FFA300>";
        else
            infoText += $"<color=#FFA300>Максимум {upgradeSetting.Value[CurrentPlayerUpgrade().Level].Value}<color=#FFA300>";

        Info.text = infoText;
        
        if(NotMaxLevel)
            Content.text = $"Цена: {upgradeSetting.Value[CurrentPlayerUpgrade().Level+1].Cost}";
        else
            Content.transform.parent.gameObject.SetActive(false);
        
        Image.sprite = upgradeSetting.Icon;

        if (withAnim && !_shakeAnim)
        {
            _shakeAnim = true;
            _button.transform.DOShakeScale(0.8f, 1.2f).onComplete += () =>
            {
                _shakeAnim = false;
            };
        }
    }

    private void Upgrade(UpgradeSetting upgradeSetting)
    {
        if(NotMaxLevel && PlayerController.Instance.TryPurchase(upgradeSetting.Value[CurrentPlayerUpgrade().Level+1].Cost))
        {
            PlayerController.Instance.Upgrade(upgradeSetting.Type);
            UpdatePanel(upgradeSetting);
        }
    }
}

public class MonoBehaviourPrefab : MonoBehaviour
{
    
}