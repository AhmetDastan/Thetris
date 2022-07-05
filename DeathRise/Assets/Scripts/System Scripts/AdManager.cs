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
    }

    private BannerView bannerAd; 

    string bannerAdId = "ca-app-pub-3940256099942544/6300978111"; // test ad => ca-app-pub-3940256099942544/6300978111   -- orj ca-app-pub-4198000366054577/2386472107 


    public void RequestBanner()
    { 
        bannerAdId = "ca-app-pub-3940256099942544/6300978111"; // test ad => ca-app-pub-3940256099942544/6300978111   -- orj ca-app-pub-4198000366054577/2386472107

        this.bannerAd = new BannerView(bannerAdId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerAd.LoadAd(request);
    }
}
