using UnityEngine;
using System;

public class Level256LuckySpin : AbsLuckySpin
{
    public Action<DeviceOrientation> onFinish;

    private DeviceOrientation _orientation;

    protected override void OnEnable()
    {
        base.OnEnable();
        delayToWaitFinishCall = 0;
    }

    protected override float GetTargetAngle()
    {

        DeviceOrientation orientation = Input.deviceOrientation;

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            orientation = DeviceOrientation.LandscapeLeft;
        }
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    orientation = DeviceOrientation.LandscapeRight;
        //}
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    orientation = DeviceOrientation.PortraitUpsideDown;
        //}
#endif
        switch (orientation)
        {
            case DeviceOrientation.PortraitUpsideDown:
                _orientation = orientation;
                return 0;
            //case DeviceOrientation.LandscapeLeft:
            //    _orientation = orientation;
            //    return 90;
            //case DeviceOrientation.LandscapeRight:
            //    _orientation = orientation;
            //    return -90;
            default:
                _orientation = DeviceOrientation.Portrait;
                return 180;
        }
    }

    protected override void WaitOnFinishCall()
    {
        onFinish?.Invoke(_orientation);
    }
}
