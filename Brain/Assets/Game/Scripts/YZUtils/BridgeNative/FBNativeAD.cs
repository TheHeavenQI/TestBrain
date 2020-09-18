
using System.Runtime.InteropServices;

public interface FBNativeADInterface {
    void showFBNativeADs();
    void hideFBNativeADs();
    void initFBNativeADsWithPlacementID(string placementID);
    bool FBNativeADsIsAdValid();
}

public class FBNativeAD {
    private static FBNativeADInterface _instance;

    public static FBNativeADInterface GetInstance() {
        if (_instance == null) {
            if (AppSetting.isIOS) {
                _instance = new FBNativeADiOS(); 
            }
            if (AppSetting.isANDROID) {
                _instance = new FBNativeADAndroid(); 
            }
        }
        return _instance;
    }
  
    public static void showFBNativeADs() {
        GetInstance()?.showFBNativeADs();
    }
    
    public static void hideFBNativeADs() {
        GetInstance()?.hideFBNativeADs();
    }
    
    public static void initFBNative() {
        GetInstance()?.initFBNativeADsWithPlacementID(AppSetting.isIOS?AppSetting.FBNativeADS_iOS:AppSetting.FBNativeADS_android);
    }
    
    public static bool FBNativeADsIsAdValid() {
        return GetInstance()?.FBNativeADsIsAdValid() ?? false;
    }
    
    
}
