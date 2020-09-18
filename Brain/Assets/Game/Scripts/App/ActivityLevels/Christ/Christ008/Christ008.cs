using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class Christ008 : ChristBaseLevel
{
    public List<GameObject> shits;

    public List<GameObject> boxs;

    public Button startBtn;

    public GameObject jiazi;

    public Text count;

    public GameObject jiaziGo;

    public GameObject jiaziBack;

    private int total = 5;

    private bool isStart = false;

    private bool isMoving = false;

    private Vector3 janziPos;

    private List<Vector3> shitsPos = new List<Vector3>();

    private Vector3 giftPos;

    private List<Tweener> shitsTweeners = new List<Tweener>();

    private List<Tweener> boxTweener = new List<Tweener>();

    private List<Tweener> jiaziTweener = new List<Tweener>();

    private bool isSuccess = false;

    private bool isRecevedCollision = false;

    private bool isUseKey = false;

    private bool isShowTips = false;

    protected override void Awake()
    {
        base.Awake();
        jiaziGo.GetComponent<GiftBoxCollider>().collisionCallBack = (bool isSu, string name) =>
        {
            if (isRecevedCollision)
            {
                return;
            }
            isRecevedCollision = true;

            if (isSu) // 夹住盒子
            {
                boxTweener[0].Kill();
                boxTweener.RemoveAt(0);

                boxs[0].transform.SetParent(jiazi.transform);
                (boxs[0].transform as RectTransform).anchoredPosition = Vector2.zero;
                isSuccess = isSu;
                return;
            }

            foreach (GameObject shit in shits)
            {
                if (shit.name == name)
                {
                    int idx = shits.IndexOf(shit);

                    shitsTweeners[idx].Kill();
                    shitsTweeners.RemoveAt(idx);

                    shit.transform.SetParent(jiazi.transform);
                    (shit.transform as RectTransform).anchoredPosition = Vector2.zero;
                }
            }

            isSuccess = isSu;

        };
    }


    public void touchScreen()
    {
        if (!isStart)
        {
            return;
        }

        if (isShowTips)
        {
            return;
        }

        if (isMoving)
        {
            return;
        }
        jiaziGoBegin();
    }

    private void startAnima()
    {
        foreach (GameObject shit in shits)
        {
            int idx = shits.IndexOf(shit);
            shitsPos.Add((shit.transform as RectTransform).anchoredPosition);

            if (idx == 0)
            {
                ShowOneShit();
            }

        }

        boxTweener.Insert(0, (boxs[0].gameObject.transform as RectTransform).DOAnchorPosX(500, 2f));
        boxTweener[0].onComplete = () =>
        {
            boxTweener.RemoveAt(0);
            boxTweener.Insert(0, (boxs[0].gameObject.transform as RectTransform).DOAnchorPosX(-500, 4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear));
        };

        giftPos = (boxs[0].transform as RectTransform).anchoredPosition;

        janziPos = (jiazi.transform as RectTransform).anchoredPosition;

    }

    public void StartButtonEvent()
    {
        startBtn.gameObject.SetActive(false);
        startAnima();
        After(() => {

            isStart = true;

        }, 0.3f);


    }

    public override void Refresh()
    {
        base.Refresh();
        startBtn.gameObject.SetActive(true);
        total = 5;
        isSuccess = false;
        isMoving = false;
        isStart = false;

        isUseKey = false;
        isRecevedCollision = false;

        count.text = "" + total;

        foreach (GameObject shit in shits)
        {
            int idx = shits.IndexOf(shit);
            (shit.transform as RectTransform).anchoredPosition = shitsPos[idx];
            
            if (idx == 0)
            {
                shit.SetActive(true);
            }else
            {
                shit.SetActive(false);
            }

        }

        foreach (GameObject gift in boxs)
        {
            int idx = boxs.IndexOf(gift);
            if (idx == 0)
            {

            }

            (gift.transform as RectTransform).anchoredPosition = giftPos;
        }

        (jiazi.transform as RectTransform).anchoredPosition = janziPos;

        StopAllAnimations();



        shitsTweeners = new List<Tweener>();
        boxTweener = new List<Tweener>();
        jiaziTweener = new List<Tweener>();

    }

    private void StopAllAnimations()
    {
        foreach (Tweener t in shitsTweeners)
        {
            t.Kill();
        }

        foreach (Tweener t in boxTweener)
        {
            t.Kill();
        }

        foreach (Tweener t in jiaziTweener)
        {
            t.Kill();
        }

    }
    int lastCount = 0;
    private void jiaziGoBegin()
    {
        isMoving = true;
        
        jiaziGo.SetActive(true);
        jiaziBack.SetActive(false);

        Tweener t = (jiazi.transform as RectTransform).DOAnchorPosY(-440, 0.75f).SetEase(Ease.Linear);
        jiaziTweener.Insert(0, t);
        t.onComplete = () =>
        {

            isRecevedCollision = false;

            jiaziTweener.RemoveAt(0);

            jiaziGo.SetActive(false);
            jiaziBack.SetActive(true);

            if (isSuccess)
            {
                total -= 1;
                count.text = "" + total;
                isSuccess = false;

            }

            Tweener t1 = (jiazi.transform as RectTransform).DOAnchorPosY(0, 0.75f).SetEase(Ease.Linear);
            jiaziTweener.Insert(0, t1);

            t1.onComplete = () => {
                isMoving = false;
                // 抓住了🎁
                if (boxs[0].transform.parent == jiazi.transform)
                {
                    boxs[0].transform.SetParent(jiazi.transform.parent);

                    (boxs[0].transform as RectTransform).anchoredPosition = giftPos;

                    boxTweener.Insert(0, (boxs[0].gameObject.transform as RectTransform).DOAnchorPosX(500, 2f));
                    boxTweener[0].onComplete = () =>
                    {
                        boxTweener.RemoveAt(0);
                        boxTweener.Insert(0, (boxs[0].gameObject.transform as RectTransform).DOAnchorPosX(-500, 4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear));
                    };
                }

                bool isCathShit = false;
                // 抓住了💩
                foreach (GameObject shit in shits)
                {
                    if (shit.transform.parent == jiazi.transform)
                    {
                        int idx = shits.IndexOf(shit);

                        shits[idx].transform.SetParent(jiazi.transform.parent);

                        (shits[idx].transform as RectTransform).anchoredPosition = shitsPos[idx];

                        ShowOneShit();

                        total = 5;
                        count.text = "" + total;
                        isSuccess = false;

                        ShowError();
                        isCathShit = true;
                    }
                }

                if (isCathShit)
                {
                    return;
                }

                if (total == 3)
                {
                    if (isUseKey)
                    {
                        isUseKey = false;
                    }
                    if(lastCount == total)
                    {
                        return;
                    }

                    ShowTwoShits();
                    
                }

                if (total == 1)
                {
                    if (isUseKey)
                    {
                        isUseKey = false;
                    }

                    if (lastCount == total)
                    {
                        return;
                    }

                    ShowThreeShits();
                }

                if (total == 0)
                {
                    Completion();
                }
                lastCount = total;
            };

        };

    }

    private void  ShowOneShit()
    {
        shitsTweeners = new List<Tweener>();

        //if (isUseKey)
        //{
        //    foreach (GameObject obj in shits)
        //    {
        //        obj.SetActive(false);
        //    }

        //    foreach (Tweener t in shitsTweeners)
        //    {
        //        t.Kill();
        //    }
        //    shitsTweeners = new List<Tweener>();

        //}
        //else
        //{
            shits[0].SetActive(true);
            shits[1].SetActive(false);
            shits[2].SetActive(false);

            Tweener t = (shits[0].gameObject.transform as RectTransform).DOAnchorPosX(500, 0.75f).SetEase(Ease.Linear);
            shitsTweeners.Insert(0, t);
            t.onComplete = () =>
            {
                shitsTweeners.RemoveAt(0);
                Tweener t1 = (shits[0].gameObject.transform as RectTransform).DOAnchorPosX(-500, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                shitsTweeners.Insert(0, t1);
            };

        //}

        
    }


    private void ShowTwoShits()
    {
        

        if (isUseKey)
        {
            foreach (Tweener t in shitsTweeners)
            {
                t.Kill();
            }
            shitsTweeners = new List<Tweener>();

            foreach (GameObject obj in shits)
            {
                obj.SetActive(false);
            }

        }
        else
        {
            shits[1].SetActive(true);

            Tweener t = (shits[1].gameObject.transform as RectTransform).DOAnchorPosX(500, 1f).SetEase(Ease.Linear);
            if (shitsTweeners.Count == 0 )
            {
                shitsTweeners.Add((shits[0].gameObject.transform as RectTransform).DOAnchorPosX(500, 1f).SetEase(Ease.Linear));
            }
            
            shitsTweeners.Insert(1, t);
            t.onComplete = () =>
            {
                shitsTweeners.RemoveAt(1);
                Tweener t1 = (shits[1].gameObject.transform as RectTransform).DOAnchorPosX(-500, 1.75f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                shitsTweeners.Insert(1, t1);
            };

        }

        
    }

    private void ShowThreeShits()
    {
        if (isUseKey)
        {
            foreach (GameObject obj in shits)
            {
                obj.SetActive(false);
            }

           foreach(Tweener t in shitsTweeners)
            {
                t.Kill();
            }
            shitsTweeners = new List<Tweener>();

        }
        else
        {
            shits[2].SetActive(true);
            Tweener t = (shits[2].gameObject.transform as RectTransform).DOAnchorPosX(500, 1.25f).SetEase(Ease.Linear);

            if (shitsTweeners.Count == 0)
            {
                shitsTweeners.Add((shits[0].gameObject.transform as RectTransform).DOAnchorPosX(500, 1f).SetEase(Ease.Linear));
            }
            if (shitsTweeners.Count == 1)
            {
                shitsTweeners.Add((shits[1].gameObject.transform as RectTransform).DOAnchorPosX(500, 1.25f).SetEase(Ease.Linear));
            }

            shitsTweeners.Insert(2, t);
            t.onComplete = () =>
            {
                shitsTweeners.RemoveAt(2);

                Tweener t1 = (shits[2].gameObject.transform as RectTransform).DOAnchorPosX(-500, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                shitsTweeners.Insert(2, t1);

            };
        }

    }

    protected override void OnTipsDialogCloseWithTipsUsed()
    {
        base.OnTipsDialogCloseWithTipsUsed();

        foreach (GameObject obj in shits)
        {
            obj.SetActive(false);
        }

        foreach (Tweener t in shitsTweeners)
        {
            t.Kill();
        }
        shitsTweeners = new List<Tweener>();

        isUseKey = true;
    }

    protected override void OnTipsDialogShow()
    {
        base.OnTipsDialogShow();

        isShowTips = true;

    }

    protected override void OnTipsDialogClose()
    {
        base.OnTipsDialogClose();
        isShowTips = false;
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text.Replace("5", "<color=#00000000>5</color>");
    }



}
