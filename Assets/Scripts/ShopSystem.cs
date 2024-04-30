using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public void ShowShopPopup()
    {
        PopupSystem.Instance.ShowPopup(new ShopPopup());
    }
}
