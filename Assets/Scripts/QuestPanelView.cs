using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanelView : RightPanelElement
{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Description;

    public void Setup(Quest quest)
    {
        UpdatePanel(quest);
        SaveManager.Instance.Loaded += () => UpdatePanel(PlayerController.Instance.Quests.First(x => x.Id == quest.Id));
    }

    private void UpdatePanel(Quest quest)
    {
        Title.text = quest.Title;
        Description.text = quest.Description;
    }
}