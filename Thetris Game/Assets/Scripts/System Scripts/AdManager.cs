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
    void RequestInterstitial() // her oyun basladiginda transectini cagir
    {
        MobileAds.Initialize(InitStatus => { });
        this.interstitial = new InterstitialAd(InterstitialAdId);

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }


    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        //reklam kapandi oyunu baslat
        //reklami yok et 
        interstitial.Destroy();
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }



}
