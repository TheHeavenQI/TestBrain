using System.Collections.Generic;

public class AdColonyPackageConfig : PackageConfig
{
    public override string Name
    {
        get { return "AdColony"; }
    }

    public override string Version
    {
        get { return /*UNITY_PACKAGE_VERSION*/"1.1.8"; }
    }

    public override Dictionary<Platform, string> NetworkSdkVersions
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, /*ANDROID_SDK_VERSION*/"4.1.4" },
                { Platform.IOS, /*IOS_SDK_VERSION*/"4.1.4" }
            };
        }
    }

    public override Dictionary<Platform, string> AdapterClassNames
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, "com.mopub.mobileads.AdColony" },
                { Platform.IOS, "AdColony" }
            };
        }
    }
}
