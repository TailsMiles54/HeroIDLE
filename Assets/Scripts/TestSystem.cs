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
    }
    #endif
}
