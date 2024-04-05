using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using YG;

public class SaveManager : MonoSingleton<SaveManager>
{
    public event Action Loaded;
    
    public void Save(PlayerController playerController)
    {
        YandexGame.savesData.Health = playerController.Health;
        YandexGame.savesData.Score = playerController.Score;
        YandexGame.savesData.Money = playerController.Money;
        YandexGame.savesData.Upgrades = playerController.Upgrades;

        YandexGame.SaveProgress();
    }

    public void TutorialComplete()
    {
        YandexGame.savesData.TutorialComplete = true;
        
        YandexGame.SaveProgress();
    }

    public bool LoadTutorialState()
    {
        return YandexGame.savesData.TutorialComplete;
    }

    public void RestoreSave()
    {
        YandexGame.savesData.Health = 100;
        YandexGame.savesData.Score = 0;
        YandexGame.savesData.Money = 0;
        YandexGame.savesData.Upgrades = new List<PlayerController.UpgradeLevel>();
        
        YandexGame.SaveProgress();
    }
    
    public void GetLoad()
    {
        var player = PlayerController.Instance;
        
        player.LoadHealth(YandexGame.savesData.Health);
        player.LoadProgress(YandexGame.savesData.Score, YandexGame.savesData.Money, YandexGame.savesData.Upgrades);
        
        Loaded?.Invoke();
    }
}
