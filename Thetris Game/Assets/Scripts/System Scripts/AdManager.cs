using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    private static AdManager _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnLevelWasLoaded()
    {
        MobileAds.Initialize(InitializationStatus => { });
        RequestBanner();
        RequestInterstitial();
    }

    private BannerView bannerAd;
    private InterstitialAd interstitial;

    string bannerAdId = "ca-app-pub-3940256099942544/6300978111";
    string InterstitialAdId = "ca-app-pub-3940256099942544/1033173712";

    private void Start()
    {
        RequestBanner();
        RequestInterstitial();
    }
    private void RequestBanner()
    {
        this.bannerAd = new BannerView(bannerAdId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerAd.LoadAd(request);
    }
    public void RequestInterstitial() // create transaction ad when player push the play button
    {
        MobileAds.Initialize(InitStatus => { });
        this.interstitial = new InterstitialAd(InterstitialAdId);

        this.interstitial.OnAdClosed += HandleInterstitialClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);

    }

    public void HandleInterstitialClosed(object sender, EventArgs args) //destroy transaction ad and begin game when player closed ad
    {
        MonoBehaviour.print("HandleAdClosed event received");

        interstitial.Destroy();
        SceneManageSystem.LoadNewScene("Game Scene");

        Debug.Log("sahne yuklenme di ");
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            RequestInterstitial();
        }
    }
}
