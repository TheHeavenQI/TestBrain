using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBNativeADCallBack : MonoBehaviour
{
    public void _nativeAdDidLoad(string placementID) {
        UtilsLog.LogWarning($"[FBNativeADCallBack]:_nativeAdDidLoad:{placementID}");
    }
    
    public void _nativeAdDidDownloadMedia() {
        UtilsLog.LogWarning($"[FBNativeADCallBack]:_nativeAdDidDownloadMedia");
    }

    public void _nativeAdWillLogImpression() {
        UtilsLog.LogWarning($"[FBNativeADCallBack]:_nativeAdWillLogImpression");
    }
    
    public void _nativeAdDidFailWithError(string error) {
        UtilsLog.LogWarning($"[FBNativeADCallBack]:_nativeAdDidFailWithError:{error}");
    }

    public void _nativeAdDidClick() {
        UtilsLog.LogWarning($"[FBNativeADCallBack]:_nativeAdDidClick");
    }

    public void _nativeAdDidFinishHandlingClick() {
        UtilsLog.LogWarning($"[FBNativeADCallBack]:_nativeAdDidFinishHandlingClick");
    }
    
}
