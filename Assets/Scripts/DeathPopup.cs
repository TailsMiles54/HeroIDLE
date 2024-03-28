using UnityEngine.SceneManagement;

public class DeathPopup : Popup<DeathPopupSettings>
{
    public override void Setup(DeathPopupSettings settings)
    {

    }

    public void ShowAd()
    {
        
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class DeathPopupSettings : BasePopupSettings
{
        
}