using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
