using System;
using System.Collections.Generic;
using System.Linq;
using BlackTailsUnityTools.Editor;
using DG.Tweening;
using UnityEngine;

public class RightPanelContentController : MonoSingleton<RightPanelContentController>
{
    private List<RightPanelElement> _rightPanelElements = new List<RightPanelElement>(); 
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _emptyPanel;

    [SerializeField] private Transform _content;
    private Vector3 _contentStartPos;

    public TabType CurrentTab { get; private set; }
    private UpgradesSettings UpgradesSettings => SettingsProvider.Get<UpgradesSettings>();

    private void Start()
    {
        _contentStartPos = _content.transform.position;
    }

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
        if(CurrentTab == tabType && !ignoreTabType)
            return;

        if (ignoreTabType)
        {
            UpdateContent();
        }
        else
        {
            _content.DOMoveY(-500, 0.8f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                UpdateContent();

                _content.DOMoveY(_contentStartPos.y, 0.5f).SetEase(Ease.OutBack);
            });   
        }

        void UpdateContent()
        {
            CurrentTab = tabType;
        
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
                    var quests = PlayerController.Instance.Quests.Where(x => !x.Completed);
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
}

public enum TabType
{
    Upgrades = 0,
    Companions = 1,
    Quests = 2,
}