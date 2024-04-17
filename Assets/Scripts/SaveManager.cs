using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using YG;

#if PLATFORM_WEBGL
using YG;
#endif

public class SaveManager : MonoSingleton<SaveManager>
{
    public event Action Loaded;
    
#if !PLATFORM_WEBGL
    public PlayerSave PlayerSave;
#endif
    
    public void Save(PlayerController playerController)
    {
#if PLATFORM_WEBGL
        YandexGame.savesData.Health = playerController.Health;
        YandexGame.savesData.Score = playerController.Score;
        YandexGame.savesData.Money = playerController.Money;
        YandexGame.savesData.Upgrades = playerController.Upgrades;
        YandexGame.SaveProgress();
#else
        PlayerSave.Health = playerController.Health;
        PlayerSave.Score = playerController.Score;
        PlayerSave.Money = playerController.Money;
        PlayerSave.Upgrades = playerController.Upgrades;
        PlayerSave.SaveProgress();
#endif
    }

    public void TutorialComplete()
    {
#if PLATFORM_WEBGL
        YandexGame.savesData.TutorialComplete = true;
        YandexGame.SaveProgress();
#else
        PlayerSave.TutorialComplete = true;
        PlayerSave.SaveProgress();
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
#else
        PlayerSave.Health = 100;
        PlayerSave.Score = 0;
        PlayerSave.Money = 0;
        PlayerSave.Upgrades = new List<PlayerController.UpgradeLevel>();
        PlayerSave.SaveProgress();
#endif
    }
    
    public void GetLoad()
    {
        var player = PlayerController.Instance;
        
#if PLATFORM_WEBGL
        player.LoadHealth(YandexGame.savesData.Health);
        player.LoadProgress(YandexGame.savesData.Score, YandexGame.savesData.Money, YandexGame.savesData.Upgrades);
#else
        PlayerSave.LoadProgress();
        player.LoadHealth(PlayerSave.Health);
        player.LoadProgress(PlayerSave.Score, PlayerSave.Money, PlayerSave.Upgrades);
#endif
        
        Loaded?.Invoke();
    }
}

[Serializable]
public class PlayerSave
{
    public List<PlayerController.UpgradeLevel> Upgrades;
    public int Money;
    public int Score;
    public float Health;
    public bool TutorialComplete;

    public void SaveProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, this);
        file.Close();
    }

    public void LoadProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);

            PlayerSave save = (PlayerSave)bf.Deserialize(file);

            Upgrades = save.Upgrades;
            Money = save.Money;
            Score = save.Score;
            Health = save.Health;
            TutorialComplete = save.TutorialComplete;
            
            file.Close();
        }
    }
}
