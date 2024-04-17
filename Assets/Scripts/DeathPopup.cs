using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#if PLATFORM_WEBGL
using YG;
#endif

public class DeathPopup : Popup<DeathPopupSettings>
{
    [SerializeField] private int RewardId;
    [SerializeField] private TMP_Text _scoreText;
    
    public override void Setup(DeathPopupSettings settings)
    {
        Time.timeScale = 0;
        _scoreText.text = $"Счёт: {settings.Score}";
        AppMetrica.Instance.ReportEvent("Dead");
    }

#if PLATFORM_WEBGL
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
#endif
    
    public void ShowAd()
    {
        
#if PLATFORM_WEBGL
        YandexGame.RewVideoShow(RewardId);
#endif
    }

    void Rewarded(int id)
    {
        if (id == RewardId)
        {
            Time.timeScale = 1;
            PlayerController.Instance.Review();
            EnemySpawner.Instance.GoToFirstEnemy();
            EnemySpawner.Instance.SpawnEnemy();
            PopupSystem.Instance.HidePopup();
        }
    }

    public void ReloadGame()
    {
        AppMetrica.Instance.ReportEvent("Reload");
        Time.timeScale = 1;
        SaveManager.Instance.RestoreSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class DeathPopupSettings : BasePopupSettings
{
    public int Score;
}