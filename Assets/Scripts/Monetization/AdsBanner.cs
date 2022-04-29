using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsBanner : MonoBehaviour
{
    private BannerView _bannerView;

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif
        _bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        _bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        _bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        _bannerView.OnAdOpening += this.HandleOnAdOpened;
        _bannerView.OnAdClosed += this.HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        _bannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetMessage());
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }
}
