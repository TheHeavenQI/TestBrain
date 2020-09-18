#if AD_MOPUB
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ADBridge.Mopub {

    public class MoPubBridge : IAdBridge {
        /// <summary>
        /// 广告加载失败后重新加载的等待时间
        /// </summary>
        internal static readonly int FAILED_RETRY_DELAY = 3;

        private MoPubListenerBanner _banner;
        private MoPubListenerInterstitial _interstitial;
        private MoPubListenerReward _reward;

        private enum InitState {
            UnInit,
            Initing,
            Inited
        }

        private InitState _initState;

        public bool IsInited => _initState == InitState.Inited;


        public void Init(IEnumerable<AdUnit> adUnits, bool debug, Action onInit) {
            if (_initState != InitState.UnInit) {
                return;
            }
            Log("Init Start");
            _initState = InitState.Initing;

            _banner = new MoPubListenerBanner();
            _interstitial = new MoPubListenerInterstitial();
            _reward = new MoPubListenerReward();

            MoPubManager.OnSdkInitializedEvent += (info) => {
                _initState = InitState.Inited;
                LoadPluginsForAdUnits(adUnits);
                onInit?.Invoke();
                Log("Init Finish");
            };
            
            MoPub.InitializeSdk(MoPubManager.Instance.SdkConfiguration);
            MoPub.ReportApplicationOpen(MoPubManager.Instance.itunesAppId);
            MoPub.EnableLocationSupport(MoPubManager.Instance.LocationAware);
            
        }

        /// <summary>
        /// Initializes a platform-specific MoPub SDK plugin
        /// </summary>
        /// <param name="adUnits"></param>
        public void LoadPluginsForAdUnits(IEnumerable<AdUnit> adUnits) {
            List<string> bannerIds = new List<string>();
            List<string> interstitialIds = new List<string>();
            List<string> rewardIds = new List<string>();
            foreach (AdUnit adUnit in adUnits) {
                Log($"{adUnit.ToString()}");
                switch (adUnit.adType) {
                    case AdType.Banner:
                        bannerIds.Add(adUnit.id);
                        break;
                    case AdType.Interstitial:
                        interstitialIds.Add(adUnit.id);
                        break;
                    case AdType.Reward:
                        rewardIds.Add(adUnit.id);
                        break;
                    default:
                        break;
                }
            }
            if (bannerIds.Count > 0) {
                MoPub.LoadBannerPluginsForAdUnits(bannerIds.ToArray());
            }
            if (interstitialIds.Count > 0) {
                MoPub.LoadInterstitialPluginsForAdUnits(interstitialIds.ToArray());
            }
            if (rewardIds.Count > 0) {
                MoPub.LoadRewardedVideoPluginsForAdUnits(rewardIds.ToArray());
            }
        }
        public void Init(string id, bool debug, Action onInit) {
            throw new Exception("[ADBridge][MoPub] Init Error, please use Init(IEnumerable<AdUnit> adUnits, bool debug, Action onInit)");
        }

        public void Request(IEnumerable<AdUnit> adUnits) {
            foreach (AdUnit adUnit in adUnits) {
                Request(adUnit);
            }
        }

        public void Request(AdUnit adUnit) {
            if (!IsInited) {
                return;
            }

            switch (adUnit.adType) {
                case AdType.Banner:
                    _banner.Request(adUnit);
                    break;
                case AdType.Interstitial:
                    _interstitial.Request(adUnit);
                    break;
                case AdType.Reward:
                    _reward.Request(adUnit);
                    break;
                default:
                    break;
            }
        }

        public void ShowAd(AdUnit adUnit) {
            if (!IsInited) {
                return;
            }

            switch (adUnit.adType) {
                case AdType.Banner:
                    MoPub.ShowBanner(adUnit.id, true);
                    break;
                case AdType.Interstitial:
                    MoPub.ShowInterstitialAd(adUnit.id);
                    break;
                case AdType.Reward:
                    MoPub.ShowRewardedVideo(adUnit.id);
                    break;
                default:
                    break;
            }
        }

        public void ShowAd(AdUnit adUnit, IAdNotify adNotify) {
            if (!IsInited) {
                return;
            }

            switch (adUnit.adType) {
                case AdType.Banner:
                    _banner.SetNotify(adNotify);
                    MoPub.ShowBanner(adUnit.id, true);
                    break;
                case AdType.Interstitial:
                    _interstitial.SetNotify(adNotify);
                    MoPub.ShowInterstitialAd(adUnit.id);
                    break;
                case AdType.Reward:
                    _reward.SetNotify((adNotify as IRewardADNotify));
                    MoPub.ShowRewardedVideo(adUnit.id);
                    break;
                default:
                    break;
            }
        }

        public void CloseAd(AdUnit adUnit) {
            if (!IsInited) {
                return;
            }

            switch (adUnit.adType) {
                case AdType.Banner:
                    MoPub.ShowBanner(adUnit.id, false);
                    break;
                case AdType.Interstitial:
                    break;
                case AdType.Reward:
                    break;
                default:
                    break;
            }
        }

        public void SetNotify(AdType adType, IAdNotify adNotify) {

            switch (adType) {
                case AdType.Banner:
                    _banner.SetNotify(adNotify);
                    break;
                case AdType.Interstitial:
                    _interstitial.SetNotify(adNotify);
                    break;
                case AdType.Reward:
                    _reward.SetNotify((adNotify as IRewardADNotify));
                    break;
                default:
                    break;
            }
        }

        public void SetAlwayNotify(AdType adType, IAdNotify adNotify) {

            switch (adType) {
                case AdType.Banner:
                    _banner.SetAlwayNotify(adNotify);
                    break;
                case AdType.Interstitial:
                    _interstitial.SetAlwayNotify(adNotify);
                    break;
                case AdType.Reward:
                    _reward.SetAlwayNotify((adNotify as IRewardADNotify));
                    break;
                default:
                    break;
            }
        }

        public bool IsAdReady(AdUnit adUnit) {
            if (!IsInited) {
                return false;
            }

            switch (adUnit.adType) {
                case AdType.Banner:
                    return true;
                case AdType.Interstitial:
                    return MoPub.IsInterstitialReady(adUnit.id);
                case AdType.Reward:
                    return MoPub.HasRewardedVideo(adUnit.id);
                default:
                    return false;
            }
        }

        internal static void Log(string info) {
            UtilsLog.Log($"[ADBridge][MoPub] {info}");
        }

        void IAdBridge.Log(string info) {
            MoPubBridge.Log(info);
        }
    }
}

#endif