using System;

namespace ADBridge {

    public enum AdType {
        Banner,
        Interstitial,
        Reward,
    }

    public struct AdUnit : IEquatable<AdUnit> {

        public readonly AdType adType;
        public readonly string id;

        public AdUnit(AdType adType, string id) {
            this.adType = adType;
            this.id = id;
        }

        public bool Equals(AdUnit other) {
            return this.adType == other.adType && this.id == other.id;
        }

        public override string ToString() {
            return $"AdUnit-{adType}-{id}";
        }
    }
}