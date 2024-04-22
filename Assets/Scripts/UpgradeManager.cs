using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private void Start()
    {
        RightPanelContentController.Instance.TabTransition(TabType.Upgrades, true);
    }
}