using System.Linq;
using UnityEngine;
#if PLATFORM_WEBGL
using YG;
#endif

public class AdsUpgradePopup : Popup<AdsUpgradePopupSettings>
{
    [SerializeField] private int RewardId;
    
    public override void Setup(AdsUpgradePopupSettings settings)
    {
        Time.timeScale = 0;
    }

    public void Close()
    {
        Time.timeScale = 1;
        PopupSystem.Instance.HidePopup();
    }

#if PLATFORM_WEBGL
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
#endif
    
    public void ShowAd()
    {
        
#if PLATFORM_WEBGL
        YandexGame.RewVideoShow(RewardId);
#endif
    }

    void Rewarded(int id)
    {
        if (id == RewardId)
        {
            Time.timeScale = 1;
            var lowestUpgrade = PlayerController.Instance.Upgrades.OrderBy(x => x.Level).First();
            PlayerController.Instance.Upgrade(lowestUpgrade.Type);
            PopupSystem.Instance.HidePopup();
        }
    }
}

public class AdsUpgradePopupSettings : BasePopupSettings
{
    
}