using System;
using System.Collections.Generic;
using Unity.VisualScripting;

#if PLATFORM_WEBGL
using YG;
#endif

public class SaveManager : MonoSingleton<SaveManager>
{
    public event Action Loaded;
    
    public void Save(PlayerController playerController)
    {
#if PLATFORM_WEBGL
        YandexGame.savesData.Health = playerController.Health;
        YandexGame.savesData.Score = playerController.Score;
        YandexGame.savesData.Money = playerController.Money;
        YandexGame.savesData.Upgrades = playerController.Upgrades;
        YandexGame.SaveProgress();
#endif
    }

    public void TutorialComplete()
    {
#if PLATFORM_WEBGL
        YandexGame.savesData.TutorialComplete = true;
        
        YandexGame.SaveProgress();
#endif
    }

    public void RestoreSave()
    {
#if PLATFORM_WEBGL
        YandexGame.savesData.Health = 100;
        YandexGame.savesData.Score = 0;
        YandexGame.savesData.Money = 0;
        YandexGame.savesData.Upgrades = new List<PlayerController.UpgradeLevel>();
        
        YandexGame.SaveProgress();
#endif
    }
    
    public void GetLoad()
    {
        var player = PlayerController.Instance;
        
        
#if PLATFORM_WEBGL
        player.LoadHealth(YandexGame.savesData.Health);
        player.LoadProgress(YandexGame.savesData.Score, YandexGame.savesData.Money, YandexGame.savesData.Upgrades);
#endif
        
        Loaded?.Invoke();
    }
}
