using BlackTailsUnityTools.Editor;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [field: SerializeField] public Transform UpgradesParent { get; private set; }
    [field: SerializeField] public GameObject EmptyPanel { get; private set; }

    private UpgradesSettings UpgradesSettings => SettingsProvider.Get<UpgradesSettings>();
    
    public static UpgradeManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var upgradeSetting in UpgradesSettings.Upgrades)
        {
            var upgradePanelPrefab = SettingsProvider.Get<PrefabsSettings>().GetObject<UpgradePanelView>();
            var upgradePanelInstance = Instantiate(upgradePanelPrefab, UpgradesParent);
            upgradePanelInstance.Setup(upgradeSetting);
        }
        
        EmptyPanel.transform.SetSiblingIndex(UpgradesParent.childCount);
    }

    public void ShowAdsUpgradePopup()
    {
        PopupSystem.Instance.ShowPopup(new AdsUpgradePopupSettings());
    }
}