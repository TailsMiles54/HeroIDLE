using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;
using SettingsProvider = BlackTailsUnityTools.Editor.SettingsProvider;

public class CompanionCreatorTool
{
#if UNITY_EDITOR
    [MenuItem("BlackTailsTools/Создать объекты компаньонов")]
    static void CreateCompanions()
    {
        var companionsSettings = SettingsProvider.Get<CompanionsSettings>();

        for (int i = 2; i <= 24; i++)
        {
            CompanionSetting companionSetting = ScriptableObject.CreateInstance<CompanionSetting>();
            companionSetting.Init();
            AssetDatabase.CreateAsset(companionSetting, $"Assets/Settings/Companions/{i}.asset");
            companionsSettings.AddCompanion(companionSetting);
        }
    }
#endif
}