using System;
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
    
    public void GetLoad()
    {
        var player = PlayerController.Instance;
        
        player.LoadHealth(YandexGame.savesData.Health);
        player.LoadProgress(YandexGame.savesData.Score, YandexGame.savesData.Money, YandexGame.savesData.Upgrades);
        
        Loaded?.Invoke();
    }
}
