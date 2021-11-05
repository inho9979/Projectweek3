using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GoogleMobileAdTest : MonoBehaviour
{
    private static GoogleMobileAdTest instance;
    public static GoogleMobileAdTest Instance
    {
        get => instance;
    }
    
    //public Button interstitialButton;
    //public Button rewardButton;
    public Text reward;

    public static readonly string interstitial1Id = "ca-app-pub-1195551850458243/5080541561"; 
    public static readonly string reward1Id = "ca-app-pub-1195551850458243/3724062018"; 

    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    private float curBGMVol;
    private float curEffectVol;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //interstitialButton.interactable = false;
        //rewardButton.interactable = false;

        InitAD();
    }

    public void InitAD()
    {
        List<string> deviceIds = new List<string>();
        deviceIds.Add("6355DBB3179CEFA7D2305B1D67D59210");
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(interstitial1Id);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void StartInterstitial()
    {
        //if (interstitial.IsLoaded())
        //{
        //    interstitial.Show();
        //}
        StartCoroutine(ShowInterstitial());
    }
    private IEnumerator ShowInterstitial()
    {
        while(!interstitial.IsLoaded())
        {
            yield return null;
        }
        interstitial.Show();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Time.timeScale = 1f;
        GameManager.Instance.BGMVolume = curBGMVol;
        GameManager.Instance.EffectVolume = curEffectVol;
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Time.timeScale = 0f;
        curBGMVol = GameManager.Instance.BGMVolume;
        curEffectVol = GameManager.Instance.EffectVolume;

        GameManager.Instance.BGMVolume = 0f;
        GameManager.Instance.EffectVolume = 0f;
        MonoBehaviour.print("HandleAdOpened event received");
        //interstitialButton.interactable = false;
    }
    public void OnClickInit()
    {
        List<string> deviceIds = new List<string>();
        deviceIds.Add("6355DBB3179CEFA7D2305B1D67D59210");
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(initStatus => { });
    }

    public void OnClickRequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(interstitial1Id);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdOpening += HandleOnAdOpened;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        //interstitialButton.interactable = true;
    }


    public void OnClickInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    public void OnClickRequestReward()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
        this.rewardedAd = new RewardedAd(reward1Id);
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        //rewardButton.interactable = true;
        reward.text = "리워드 광고 출력";
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        //rewardButton.interactable = false;
    }

    public void OnClickReward()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        reward.text = "received for " + amount.ToString() + " " + type;
    }
}
