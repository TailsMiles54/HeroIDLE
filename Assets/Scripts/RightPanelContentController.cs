using System;
using System.Collections.Generic;
using BlackTailsUnityTools.Editor;
using UnityEngine;

public class RightPanelContentController : MonoSingleton<RightPanelContentController>
{
    private List<RightPanelElement> _rightPanelElements = new List<RightPanelElement>(); 
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _emptyPanel;

    private TabType _currentTab;
    private UpgradesSettings UpgradesSettings => SettingsProvider.Get<UpgradesSettings>();
    
    public void QuestTab()
    {
        TabTransition(TabType.Quests);
    }
    
    public void UpgradesTab()
    {
        TabTransition(TabType.Upgrades);
    }
    
    public void CompanionsTab()
    {
        TabTransition(TabType.Companions);
    }
    
    public void TabTransition(TabType tabType, bool ignoreTabType = false)
    {
        if(_currentTab == tabType && !ignoreTabType)
            return;
        
        _currentTab = tabType;
        
        _rightPanelElements.ForEach(x => Destroy(x.gameObject));
        _rightPanelElements.Clear();
        
        switch (tabType)
        {
            case TabType.Upgrades:
                foreach (var upgradeSetting in UpgradesSettings.Upgrades)
                {
                    var upgradePanelPrefab = SettingsProvider.Get<PrefabsSettings>().GetObject<UpgradePanelView>();
                    var upgradePanelInstance = Instantiate(upgradePanelPrefab, _parent);
                    upgradePanelInstance.Setup(upgradeSetting);
                    _rightPanelElements.Add(upgradePanelInstance);
                }
        
                break;
            case TabType.Companions:
                break;
            case TabType.Quests:
                var quests = PlayerController.Instance.Quests;
                foreach (var quest in quests)
                {
                    var upgradePanelPrefab = SettingsProvider.Get<PrefabsSettings>().GetObject<QuestPanelView>();
                    var upgradePanelInstance = Instantiate(upgradePanelPrefab, _parent);
                    upgradePanelInstance.Setup(quest);
                    _rightPanelElements.Add(upgradePanelInstance);
                }

                break;
        }
        _emptyPanel.transform.SetSiblingIndex(_parent.childCount);
    }
}

public enum TabType
{
    Upgrades = 0,
    Companions = 1,
    Quests = 2,
}