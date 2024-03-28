using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerInfoPanel : MonoBehaviour
{
    [field: SerializeField] public Scrollbar HealthBar { get; private set; }
    [field: SerializeField] public TMP_Text Money { get; private set; }

    public void UpdatePanel(PlayerController playerController)
    {
        HealthBar.value = playerController.Health / playerController.MaxHealth;
        Money.text = $"Money: {playerController.Money} Score: {playerController.Score}";
    }
}
