using System;
using UnityEngine;

public class BasePopUp : BaseCanvas {
    protected override int showType => 2;
    protected bool showNative = true;
    protected override float marginY => showNativeAD ? 220 : 0;
    protected virtual bool showNativeAD => showNative && FBNativeAD.FBNativeADsIsAdValid();
    
    public override void viewWillAppear() {
        base.viewWillAppear();
        UtilsLog.LogWarning($"showNative:{showNative}");
        if (showNativeAD) {
            ADManager.CloseAD(GameAdID.Banner);
            FBNativeAD.showFBNativeADs();
        }
    }

    public override void viewWillDisappear() {
        base.viewWillDisappear();
        FBNativeAD.hideFBNativeADs();
        ContentController.Instance.currentLevelPage.RefreshBanner();
    }
}
