using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADBridge {

    public enum MediationType {
        MoPub
    }

    public static class AdBridge {

        private static IAdBridge _bridge;

        static AdBridge() {
            new GameObject("AdBridge_Loom").AddComponent<Loom>();
        }

        public static void InitMediationType(MediationType mediationType) {
            switch (mediationType) {
#if AD_IRONSOURCE
                case MediationType.IronSource: _bridge = new ADBridge.Ironsouce.IronSourceBridge(); break;
#endif
#if AD_MOPUB
                case MediationType.MoPub: _bridge = new ADBridge.Mopub.MoPubBridge(); break;
#endif
                default: break;
            }
        }

        public static bool IsInited => _bridge.IsInited;

        /// <summary>
        /// Init Bridge (Mopub), you must execute <see cref="InitMediationType"/> before this method
        /// </summary>
        /// <param name="adUnits"></param>
        /// <param name="debug"></param>
        /// <param name="onInit"></param>
        public static void Init(IEnumerable<AdUnit> adUnits, bool debug, Action onInit) {
            if (_bridge == null) {
                throw new Exception("Init Bridge Error, you must execute InitMediationType before this method");
            }
            _bridge.Init(adUnits, debug, onInit);
        }

        /// <summary>
        /// Init Bridge (IronSource), you must execute <see cref="InitMediationType"/> before this method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="debug"></param>
        /// <param name="onInit"></param>
        public static void Init(string id, bool debug, Action onInit) {
            if (_bridge == null) {
                throw new Exception("Init Bridge Error, you must execute InitMediationType before this method");
            }
            _bridge.Init(id, debug, onInit);
        }

        public static void Request(IEnumerable<AdUnit> adUnits) {
            _bridge?.Request(adUnits);
        }

        public static void Request(AdUnit adUnit) {
            _bridge?.Request(adUnit);
        }

        public static void ShowAd(AdUnit adUnit) {
            _bridge?.ShowAd(adUnit);
        }

        public static void ShowAd(AdUnit adUnit, IAdNotify adNotify) {
            _bridge?.ShowAd(adUnit, adNotify);
        }

        public static void CloseAd(AdUnit adUnit) {
            _bridge?.CloseAd(adUnit);
        }

        public static void SetNotify(AdType adType, IAdNotify adNotify) {
            _bridge?.SetNotify(adType, adNotify);
        }

        public static void SetAlwayNotify(AdType adType, IAdNotify adNotify) {
            _bridge?.SetAlwayNotify(adType, adNotify);
        }

        public static bool IsAdReady(AdUnit adUnit) {
            if (_bridge == null) {
                return false;
            }
            return _bridge.IsAdReady(adUnit);
        }

        internal static void Log(string info) {
            _bridge?.Log(info);
        }
    }
}