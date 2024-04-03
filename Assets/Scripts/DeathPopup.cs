using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class DeathPopup : Popup<DeathPopupSettings>
{
    [SerializeField] private int RewardId;
    [SerializeField] private TMP_Text _scoreText;
    
    public override void Setup(DeathPopupSettings settings)
    {
        Time.timeScale = 0;
        _scoreText.text = $"Счёт: {settings.Score}";
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
            Time.timeScale = 1;
            PlayerController.Instance.Review();
            EnemySpawner.Instance.GoToFirstEnemy();
            EnemySpawner.Instance.SpawnEnemy();
            PopupSystem.Instance.HidePopup();
        }
    }

    public void ReloadGame()
    {
        Time.timeScale = 1;
        SaveManager.Instance.RestoreSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class DeathPopupSettings : BasePopupSettings
{
    public int Score;
}