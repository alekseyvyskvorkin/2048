using System;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Threading.Tasks;

public class AdsInterstitial : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    private InterstitialAd _interstitial;

    private void Start()
    {
        RequestInterstitial();
    }

    private void OnDestroy()
    {
        _interstitial.Destroy();
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#endif

        _interstitial = new InterstitialAd(adUnitId);

        _interstitial.OnAdLoaded += HandleOnAdLoaded;
        _interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        _interstitial.OnAdOpening += HandleOnAdOpening;
        _interstitial.OnAdClosed += HandleOnAdClosed;

        LoadAds();
    }

    private void LoadAds()
    {
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }

    [ContextMenu("Request")]
    public void ShowInterstitial()
    {
        if (_interstitial.IsLoaded())
        {
            Debug.Log("Show");
            _inputController.enabled = false;
            _interstitial.Show();
        }
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

    public async void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");

        LoadAds();

        await Task.Delay(500);
        _inputController.enabled = true;
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }
}
