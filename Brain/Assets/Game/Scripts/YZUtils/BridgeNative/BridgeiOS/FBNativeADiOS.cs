
using System.Runtime.InteropServices;

public class FBNativeADiOS:FBNativeADInterface
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void _initFBNativeADsWithPlacementID (string placementID);
    [DllImport("__Internal")]
    private static extern void _showFBNativeADs ();
    [DllImport("__Internal")]
    private static extern void _hideFBNativeADs ();
    [DllImport("__Internal")]
    private static extern bool _FBNativeADsIsAdValid();
    [DllImport("__Internal")]
    private static extern void  _FBNativeADsLoad();
#endif
    
    public void showFBNativeADs() {
#if UNITY_IOS && !UNITY_EDITOR
        _showFBNativeADs();
#endif
    }

    public void hideFBNativeADs() {
#if UNITY_IOS && !UNITY_EDITOR
        _hideFBNativeADs();
#endif
    }

    public  void initFBNativeADsWithPlacementID(string placementID) {
#if UNITY_IOS && !UNITY_EDITOR
        _initFBNativeADsWithPlacementID(placementID);
#endif
    }
    
    public bool FBNativeADsIsAdValid() {
        if (Global.isHideAD) {
            return false;
        }
    #if UNITY_IOS && !UNITY_EDITOR
           return _FBNativeADsIsAdValid();
    #endif
        return false;
    }
    
    
}
