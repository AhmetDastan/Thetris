//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
//using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    private static AdManager _instance = null;

    private void Awake()
    { 
        if (_instance == null)
        {
            MobileAds.Initialize(initStatus => { }); 
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        { 
            Destroy(gameObject);
        }
        this.RequestBanner();
        //this.RequestInterstitial();
    }

    private BannerView bannerAd;
   // private InterstitialAd interstitial;

    string bannerAdId = "ca-app-pub-3940256099942544/6300978111"; // test ad => ca-app-pub-3940256099942544/6300978111   -- orj ca-app-pub-4198000366054577/2386472107
    //string InterstitialAdId = "test ad => ca-app-pub-3940256099942544/1033173712";  //test ad => ca-app-pub-3940256099942544/1033173712  -- otj ca-app-pub-4198000366054577/4673724096


    public void RequestBanner()
    { 
        bannerAdId = "ca-app-pub-4198000366054577/2386472107"; // test ad => ca-app-pub-3940256099942544/6300978111   -- orj ca-app-pub-4198000366054577/2386472107

        this.bannerAd = new BannerView(bannerAdId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerAd.LoadAd(request);
    }
   /* public void RequestInterstitial() // create transaction ad when player push the play button
    {
        InterstitialAdId = "ca-app-pub-4198000366054577/4673724096";  //test ad => ca-app-pub-3940256099942544/1033173712  -- otj ca-app-pub-4198000366054577/4673724096

        this.interstitial = new InterstitialAd(InterstitialAdId);

        this.interstitial.OnAdClosed += HandleInterstitialClosed;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);

    }

    public void HandleInterstitialClosed(object sender, EventArgs args) //destroy transaction ad and begin game when player closed ad
    {
        MonoBehaviour.print("HandleAdClosed event received");
        Debug.Log("intiliza " + interstitial.IsLoaded());
        Debug.Log("destroyed intiliza");
        interstitial.Destroy();
        SceneManageSystem.LoadNewScene("Game Scene");
        Debug.Log("intiliza " + interstitial.IsLoaded());

        RequestInterstitial();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("reklam yuklenemedi gec oyun scene");
        SceneManageSystem.LoadNewScene("Game Scene");
    }
    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            interstitial.Destroy();
            RequestInterstitial();
            SceneManageSystem.LoadNewScene("Game Scene");
        }
    }*/
}
