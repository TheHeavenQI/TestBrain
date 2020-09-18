using System.Collections.Generic;

public class ChartboostPackageConfig : PackageConfig
{
    public override string Name
    {
        get { return "Chartboost"; }
    }

    public override string Version
    {
        get { return /*UNITY_PACKAGE_VERSION*/"1.1.17"; }
    }

    public override Dictionary<Platform, string> NetworkSdkVersions
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, /*ANDROID_SDK_VERSION*/"8.0.2" },
                { Platform.IOS, /*IOS_SDK_VERSION*/"8.1.0" }
            };
        }
    }

    public override Dictionary<Platform, string> AdapterClassNames
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, "com.mopub.mobileads.Chartboost" },
                { Platform.IOS, "Chartboost" }
            };
        }
    }
}
