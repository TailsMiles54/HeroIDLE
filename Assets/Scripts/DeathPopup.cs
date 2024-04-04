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
    
    public void ShowAd()
    {
        Debug.Log("Test3");
        AdManager.ShowAd();
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