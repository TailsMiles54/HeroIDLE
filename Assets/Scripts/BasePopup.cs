using UnityEngine;

public class BasePopup : MonoBehaviourPrefab
{
    public virtual void Hide()
    {
        Destroy(gameObject);
    }
}