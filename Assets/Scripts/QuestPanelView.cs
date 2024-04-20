using System.Linq;
using TMPro;
using UnityEngine;

public class QuestPanelView : RightPanelElement
{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Progress;

    private Quest _quest;
    public void Setup(Quest quest)
    {
        _quest = quest;
        UpdatePanel();
        _quest.ProgressChanged += UpdatePanel;
    }

    private void UpdatePanel()
    {
        Title.text = _quest.Title;
        Description.text = _quest.Description;
        Progress.text = $"{_quest.Progress}/{_quest.QuestSetting.TargetCount}";
    }

    private void OnDestroy()
    {
        _quest.ProgressChanged -= UpdatePanel;
    }
}