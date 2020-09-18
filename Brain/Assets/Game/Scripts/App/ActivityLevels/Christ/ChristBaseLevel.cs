using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristBaseLevel : LevelBasePage
{
    /// <summary>
    /// 是否成功使用tips
    /// </summary>
    private bool _isTipsUsed = false;

    protected virtual void OnEnable()
    {
        EventCenter.AddListener(UtilsEventType.OnTipsDialogShow, OnTipsDialogShow);
        EventCenter.AddListener<int>(UtilsEventType.OnTipNumModify, OnTipNumModify);
        EventCenter.AddListener(UtilsEventType.OnTipsDialogClose, OnTipsDialogClose);
    }
    protected virtual void OnDisable()
    {
        EventCenter.RemoveListener(UtilsEventType.OnTipsDialogShow, OnTipsDialogShow);
        EventCenter.RemoveListener<int>(UtilsEventType.OnTipNumModify, OnTipNumModify);
        EventCenter.RemoveListener(UtilsEventType.OnTipsDialogClose, OnTipsDialogClose);
    }

    /// <summary>
    /// tips弹窗出现，注意不一定是提示成功
    /// </summary>
    protected virtual void OnTipsDialogShow()
    {

    }

    /// <summary>
    /// 可用tips个数加减，modifyNum为-1时使用tips成功
    /// </summary>
    /// <param name="modifyNum">tips加减个数</param>
    protected virtual void OnTipNumModify(int modifyNum)
    {
        if (modifyNum == -1)
        {
            _isTipsUsed = true;
        }
    }

    /// <summary>
    /// tips弹窗关闭，注意不一定是提示成功
    /// </summary>
    protected virtual void OnTipsDialogClose()
    {
        if (_isTipsUsed)
        {
            OnTipsDialogCloseWithTipsUsed();
            _isTipsUsed = false;
        }
    }

    /// <summary>
    /// tips弹窗关闭，关闭前成功使用了tips
    /// </summary>
    protected virtual void OnTipsDialogCloseWithTipsUsed()
    {

    }
}
