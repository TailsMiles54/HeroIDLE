using System;
using Mycom.Target.Unity.Ads;
using UnityEngine;

public static class AdManager
{
    private static InterstitialAd CreateInterstitialAd()
    {
        UInt32 slotId = 0;
#if UNITY_ANDROID
        slotId = 1537595;
#endif
        // Включение режима отладки
        // InterstitialAd.IsDebugMode = true;
        // Создаем экземпляр InterstitialAd
        return new InterstitialAd(slotId);
    }
    
    private static InterstitialAd _interstitialAd;

    public static void InitAd()
    {
        // Создаем экземпляр InterstitialAd
        _interstitialAd = CreateInterstitialAd();
    
        // Устанавливаем обработчики событий
        _interstitialAd.AdLoadCompleted += OnLoadCompleted;
        _interstitialAd.AdDisplayed += OnAdDisplayed;
        _interstitialAd.AdDismissed += OnAdDismissed;
        _interstitialAd.AdVideoCompleted += OnAdVideoCompleted;
        _interstitialAd.AdClicked += OnAdClicked;
        _interstitialAd.AdLoadFailed += OnAdLoadFailed;
    
        // Запускаем загрузку данных
        _interstitialAd.Load();
        Debug.Log("Initialized");
    }

    private static void OnLoadCompleted(object sender, EventArgs e)
    {
        Debug.Log("LoadCompleted");
        // на отдельной странице
        _interstitialAd.Show();

        // или в диалоговом окне
        // _interstitialAd.ShowDialog();
    }

    public static void ShowAd()
    {
        Debug.Log("ShowAd");
        _interstitialAd.Show();
    }
    
    private static void OnAdDisplayed(object sender, EventArgs e)
    {
    
    }

    private static void OnAdDismissed(object sender, EventArgs e)
    {
    
    }
 
    private static void OnAdVideoCompleted(object sender, EventArgs e)
    {

    }

    private static void OnAdClicked(object sender, EventArgs e)
    {
         
    }

    private static void OnAdLoadFailed(object sender, ErrorEventArgs e)
    {
        Debug.Log("OnAdLoadFailed: " + e.Message);
    }
}