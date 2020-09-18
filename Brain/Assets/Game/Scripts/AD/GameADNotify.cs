using System;
using System.Collections;
using System.Collections.Generic;
using ADBridge;
using UnityEngine;

#region ADNotify
public class ADNotify : IAdNotify {

    public Action onAdClick;
    public Action onAdClose;
    public Action onAdLoad;
    public Action onAdLoadFailed;
    public Action onAdShow;

    public void OnAdClick() {
        onAdClick?.Invoke();
    }

    public virtual void OnAdClose() {
        onAdClose?.Invoke();
    }

    public void OnAdLoad() {
        onAdLoad?.Invoke();
    }

    public void OnAdLoadFailed() {
        onAdLoadFailed?.Invoke();
    }
    
    public void OnAdShow() {
        onAdShow?.Invoke();
    }
}

public class RewardADNotify : ADNotify, IRewardADNotify {

    public Action onAdReward;
    public Action onAdSkip;

    private bool _isRewarded = false;

    public void OnAdReward() {
        _isRewarded = true;
    }

    public override void OnAdClose() {
        if (_isRewarded) {
            onAdReward?.Invoke();
        } else {
            onAdSkip?.Invoke();
        }
        _isRewarded = false;
        base.OnAdClose();
    }
}

#endregion ADNotify
