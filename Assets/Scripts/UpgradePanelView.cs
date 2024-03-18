using TMPro;
using UnityEngine;

public class UpgradePanelView : MonoBehaviourPrefab
{
    [field: SerializeField] public TMP_Text Title;
    [field: SerializeField] public TMP_Text Info;
    [field: SerializeField] public TMP_Text Content;
    
    public void Setup(UpgradeSetting upgradeSetting)
    {
        Title.text = upgradeSetting.Type.ToString();
        Info.text = "крутой бафф";
        Content.text = "0/5";
    }
}

public class MonoBehaviourPrefab : MonoBehaviour
{
    
}