using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADBridge {

    public interface IAdBridge {

        bool IsInited { get; }

        void Init(IEnumerable<AdUnit> adUnits, bool debug, Action onInit);

        void Init(string id, bool debug, Action onInit);

        void Request(IEnumerable<AdUnit> adUnits);

        void Request(AdUnit adUnit);

        void ShowAd(AdUnit adUnit);

        void ShowAd(AdUnit adUnit, IAdNotify adNotify);

        void CloseAd(AdUnit adUnit);

        void SetNotify(AdType adType, IAdNotify adNotify);

        void SetAlwayNotify(AdType adType, IAdNotify adNotify);

        bool IsAdReady(AdUnit adUnit);

        void Log(string info);
    }
}