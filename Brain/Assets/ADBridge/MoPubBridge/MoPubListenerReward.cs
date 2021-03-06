﻿#if AD_MOPUB

using ADBridge;

namespace ADBridge.Mopub {

    internal class MoPubListenerReward {
        private IRewardADNotify _adTempNotify;
        private IRewardADNotify _adAlwayNotify;

        public MoPubListenerReward() {
            MoPubManager.OnRewardedVideoLoadedEvent += OnAdLoad;
            MoPubManager.OnRewardedVideoFailedEvent += OnAdLoadFailed;
            MoPubManager.OnRewardedVideoClickedEvent += OnAdClick;
            MoPubManager.OnRewardedVideoShownEvent += OnAdShow;
            MoPubManager.OnRewardedVideoClosedEvent += OnAdClose;
            MoPubManager.OnRewardedVideoReceivedRewardEvent += OnAdReward;
            MoPubManager.OnImpressionTrackedEvent += (s, data) => {
                Loom.QueueOnMainThread(() => {
                    var NetworkPlacementId = data.NetworkPlacementId;
                    var NetworkName = data.NetworkName;
                    double PublisherRevenue = data.PublisherRevenue ?? -1;
                    int type = 0;
                    if (data.AdUnitFormat == "Banner") {
                        type = 1;
                    }else if (data.AdUnitFormat == "Fullscreen") {
                        type = 2;
                    }else if (data.AdUnitFormat == "Rewarded Video") {
                        type = 3;
                    }
                    MoPubBridge.Log($"OnImpressionTrackedEvent {s} {data.JsonRepresentation}");
                    AnalyticsUtil.Log("OnImpressionTrackedEvent","data",data.JsonRepresentation);
                    WudiAnalytics.WatchADReq(type,NetworkName,NetworkPlacementId,PublisherRevenue,data.Currency);
                });
            };
        }

        public void SetNotify(IRewardADNotify adNotify) {
            this._adTempNotify = adNotify;
        }

        public void SetAlwayNotify(IRewardADNotify adNotify) {
            this._adAlwayNotify = adNotify;
        }

        public void Request(AdUnit adUnit) {
            MoPub.RequestRewardedVideo(adUnit.id);
            MoPubBridge.Log("Reward Request");
        }

        private void OnAdLoad(string id) {
            Loom.QueueOnMainThread(() => {
                _adTempNotify?.OnAdLoad();
                _adAlwayNotify?.OnAdLoad();
                MoPubBridge.Log("Reward OnLoaded");
            });
        }

        private void OnAdLoadFailed(string id, string error) {
            Loom.QueueOnMainThread(() => {
                _adTempNotify?.OnAdLoadFailed();
                _adAlwayNotify?.OnAdLoadFailed();
                MoPubBridge.Log($"Reward OnLoaded Failed {error}");
            });
            Loom.QueueOnMainThread(() => MoPub.RequestRewardedVideo(id), MoPubBridge.FAILED_RETRY_DELAY);
        }

        private void OnAdClick(string id) {
            Loom.QueueOnMainThread(() => {
                _adTempNotify?.OnAdClick();
                _adAlwayNotify?.OnAdClick();
                MoPubBridge.Log("Reward OnClick");
            });
        }

        private void OnAdShow(string id) {
            Loom.QueueOnMainThread(() => {
                _adTempNotify?.OnAdShow();
                _adAlwayNotify?.OnAdShow();
                MoPubBridge.Log("Reward OnShow");
            });
        }

        private void OnAdClose(string id) {
            Loom.QueueOnMainThread(() => {
                _adTempNotify?.OnAdClose();
                _adAlwayNotify?.OnAdClose();
                MoPub.RequestRewardedVideo(id);
                MoPubBridge.Log("Reward OnAdClose");
            });
        }

        private void OnAdReward(string id, string info, float value) {
            Loom.QueueOnMainThread(() => {
                _adTempNotify?.OnAdReward();
                _adAlwayNotify?.OnAdReward();
                MoPubBridge.Log("Reward OnAdReward");
            });
        }
    }
}
#endif