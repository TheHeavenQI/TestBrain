using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level143 : LevelBasePage
{
    public CommonCar mCommonCar;
    Vector2 commonCarDefPos;
    public Image mCommonCarImage;

    public Image mEmptyCar;
    Vector2 emptyCarDefPos;
    public Image mDriver;
    Vector2 driverDefPos;

    public GameObject mNormalState;
    public GameObject mBrokenState;

    protected override void Start()
    {
        base.Start();
        commonCarDefPos = mCommonCar.transform.localPosition;
        emptyCarDefPos = mEmptyCar.rectTransform.anchoredPosition;
        driverDefPos = mDriver.rectTransform.anchoredPosition;
        mCommonCar.isAutoMove = true;
        mCommonCar.onClick = OntouchDownCar;
    }
    void OntouchDownCar()
    {
        if (!mCommonCarImage.enabled)
            return;

        if (mCommonCar.isAutoMove)
        {
            mCommonCar.isAutoMove = false;
            return;
        }
        mCommonCarImage.enabled = false;
        mEmptyCar.enabled = mDriver.enabled = true;
        mDriver.transform.DOLocalMoveX(800,3);
        After(() => {
            Completion();
        }, 0.5f);
    }
    bool inFailedState = false;
    private void Update()
    {
        if (!mCommonCar.isAutoMove)
            return;
        if (inFailedState)
            return;
        if (mCommonCar.transform.localPosition.x > 0)
        {
            inFailedState = true;
            mNormalState.SetActive(false);
            mBrokenState.SetActive(true);
            ShowError();
            After(() =>
            {
                Refresh();
            }, 2f);
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        mCommonCar.transform.localPosition = commonCarDefPos;
        mEmptyCar.rectTransform.anchoredPosition = emptyCarDefPos;
        mDriver.rectTransform.anchoredPosition = driverDefPos;
        mCommonCar.isAutoMove = true;
        inFailedState = false;
        mNormalState.SetActive(true);
        mBrokenState.SetActive(false);
    }
}
