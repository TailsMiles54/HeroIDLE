using System;
using System.Linq;
using BlackTailsUnityTools.Editor;
using DG.Tweening;
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

    private bool _shakeAnim;
    
    private UpgradeSetting.UpgradeType _upgradeType;
    private PlayerController.UpgradeLevel CurrentPlayerUpgrade => PlayerController.Instance.Upgrades.First(x => x.Type == _upgradeType);
    
    public void Setup(UpgradeSetting upgradeSetting)
    {
        UpdatePanel(upgradeSetting);
        _button.onClick.AddListener(() => Upgrade(upgradeSetting));
        SaveManager.Instance.Loaded += () => UpdatePanel(SettingsProvider.Get<UpgradesSettings>().Upgrades.First(x => x.Type == _upgradeType));
        PlayerController.Instance.Upgraded += (upgradeType) =>
        {
            if(upgradeType == _upgradeType)
                UpdatePanel(SettingsProvider.Get<UpgradesSettings>().Upgrades.First(x => x.Type == _upgradeType), true);
        };
    }

    private void UpdatePanel(UpgradeSetting upgradeSetting, bool withAnim = false)
    {
        _upgradeType = upgradeSetting.Type; 
        Title.text = upgradeSetting.Name;
        Info.text = $"{CurrentPlayerUpgrade.Level}/{upgradeSetting.Value.Count}" +
                    $"\n{upgradeSetting.Value[CurrentPlayerUpgrade.Level].Value} -> <color=#FFA300>{upgradeSetting.Value[CurrentPlayerUpgrade.Level+1].Value}<color=#FFA300>";
        Content.text = $"Цена: {upgradeSetting.Value[CurrentPlayerUpgrade.Level+1].Cost}";
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