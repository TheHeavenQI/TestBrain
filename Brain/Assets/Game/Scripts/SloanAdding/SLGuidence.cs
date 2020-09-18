using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class SLGuidence : MonoBehaviour
{

    private float startTime = 0.0f;
    private bool isSHow = false;

    public GameObject hand;

    private int levelIndex;

    private void Start()
    {
        startTime = Time.time;
        levelIndex = gameObject.GetComponent<LevelBasePage>().levelIndex;
    }

    private void FixedUpdate()
    {
        if (GuidenceManager.Instance().canShowGuide)
        {

            if (!isSHow && Time.time - startTime > 8 && levelIndex == GuidenceManager.Instance().guideCount + 1)
            {
                isSHow = true;

                switch (GuidenceManager.Instance().guideCount) {

                    case 0:
                        ShowFirstDuidence();
                        break;
                    case 1:
                        ShowSecondDuidence();
                        break;
                    default:
                        break;
                }

            }
        }

    }

    private void ShowFirstDuidence()
    {
        if (hand != null)
        {
            hand.gameObject.SetActive(true);
            (hand.transform as RectTransform).DOScale(1.2f, 0.75f).SetLoops(-1, LoopType.Yoyo);
            
        }
    }

    private void ShowSecondDuidence()
    {
        if(hand != null)
        {
            hand.gameObject.SetActive(true);
            (hand.transform as RectTransform).DOScale(1.2f, 0.75f).SetLoops(-1, LoopType.Yoyo);
            
        }
    }

}
