using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdsManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private BannerView bannerView;

    public bool isRewardOkay;

    #region Singleton
    public static GoogleAdsManager Instance;
    

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => {});
        RequestInterstitial();
        RequestRewardedAd();
        requestBanner();
    }

    private void RequestRewardedAd()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        isRewardOkay = true;
        GameManager.Instance.canCountinue = true;
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isRewardOkay = false;
        GameManager.Instance.canCountinue = false;
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        isRewardOkay = true;
        GameManager.Instance.canCountinue = true;
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        isRewardOkay = false;
        GameManager.Instance.canCountinue = false;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        isRewardOkay = false;
        GameManager.Instance.canCountinue = false;
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.canCountinue = true;
        isRewardOkay = true;

    }
    public void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }


    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
            RequestRewardedAd();
        }
    }

    public bool HandleRewardedAdOpening()
    {
        return true;
    }

    private void requestBanner()
    {
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
    #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
    #else
            string adUnitId = "unexpected_platform";
    #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}
