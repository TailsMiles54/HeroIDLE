using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
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
            if (Input.GetKeyUp(KeyCode.M))
            {
                PlayerController.Instance.AddReward(10000,0);
            }
        }
    }
    #endif
}
