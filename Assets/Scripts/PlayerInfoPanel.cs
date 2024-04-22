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
    [field: SerializeField] public TMP_Text Health { get; private set; }
    [field: SerializeField] public LandedProgressBar LandedProgressBarAutoAttackTime { get; private set; }

    public void UpdatePanel(PlayerController playerController)
    {
        Health.text = $"{(int)playerController.Health}/{(int)playerController.MaxHealth}";
        HealthBar.value = playerController.Health / playerController.MaxHealth;
        Money.text = $"Деньги: {playerController.Money} Счёт: {playerController.Score}";
    }
    
    public void UpdateAutoAttackTimeBar(float time)
    {
        LandedProgressBarAutoAttackTime.ToMinWithTime(time);
    }
}
