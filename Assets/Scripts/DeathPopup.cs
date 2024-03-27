using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class DeathPopup : Popup<DeathPopupSettings>
{
    [SerializeField] private int RewardId;
    
    public override void Setup(DeathPopupSettings settings)
    {

    }

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    
    public void ShowAd()
    {
        YandexGame.RewVideoShow(RewardId);
    }

    void Rewarded(int id)
    {
        if (id == RewardId)
        {
            PlayerController.Instance.Review();
            EnemySpawner.Instance.GoToFirstEnemy();
            EnemySpawner.Instance.SpawnEnemy();
            PopupSystem.Instance.HidePopup();
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class DeathPopupSettings : BasePopupSettings
{
        
}