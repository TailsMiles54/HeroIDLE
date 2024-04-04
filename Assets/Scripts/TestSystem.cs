using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private TMP_Text _tmp;
    
    private void Awake()
    {
        Debug.Log("Test1");
        AdManager.InitAd();
        Application.logMessageReceived += LogCallback;
        Debug.Log("Test2");
    }

    private void LogCallback(string condition, string stacktrace, LogType type)
    {
        var text = Instantiate(_tmp, _parent);
        text.text = $"{stacktrace}\n-----------------------------\n";
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                BattleManager.Instance.NextEnemy();
            }
        }
        
        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyUp(KeyCode.U))
            {
                PlayerController.Instance.Upgrade(UpgradeSetting.UpgradeType.AutoAttackSpeed);
            }
        }
        
        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                AdManager.ShowAd();
            }
        }
    }
    #endif
}
