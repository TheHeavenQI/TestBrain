using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;
public class UtilsHud {
    public static float _hudTime = 0.7f;
    private static int _waitedNum = 0;

    /// <summary>
    /// 显示成功
    /// </summary>
    public static void ShowSuccess(Transform parentTransform, Vector3 vector) {

        GameObjectPoolSingle pool = GameObjectPool.GetPool("SuccessImage");
        GameObject obj = pool.Get();
        obj.transform.SetParent(parentTransform, false);
        obj.transform.localScale = new Vector3(0, 0, 0);
        obj.transform.localPosition = vector;
        obj.transform.DOScale(1, _hudTime)
            .OnComplete(() => {
                GameObjectPool.After(() => {
                    if (obj != null)
                        GameObjectPool.RemoveObject(obj);
                }, 5);
            });
    }
   
    public static void ShowError(Transform parentTransform,Vector3 vector) {
        Utils.Vibrate();
        GameObjectPoolSingle pool = GameObjectPool.GetPool("ErrorImage");
        GameObject obj = pool.Get();
        obj.transform.SetParent(parentTransform,false);
        obj.transform.localScale = new Vector3(1,1,1);
        obj.transform.localPosition = vector;
        obj.transform.DOScale(0, _hudTime)
            .From()
            .SetLoops(2,LoopType.Yoyo)
            .OnComplete(()=> {
                GameObjectPool.RemoveObject(obj);
            });
    }
    
    public static void ShowTap(Transform parentTransform,Vector3 vector) {
        GameObjectPoolSingle pool = GameObjectPool.GetPool("PointDot");
        GameObject obj = pool.Get();
        obj.transform.SetParent(parentTransform,false);
        obj.transform.localScale = new Vector3(1,1,1);
        obj.transform.localPosition = vector;
        Image im = obj.transform.GetComponent<Image>();
        im.color = new Color(im.color.r,im.color.g,im.color.b,0.5f);
        im.DOFade(0, 0.2f).SetDelay(0.1f);
        obj.SetActive(true);
        obj.transform.localScale = Vector3.one * 0.2f;
        obj.transform.DOScale(1f, 0.3f).OnComplete(() => {
           GameObjectPool.RemoveObject(obj);
        });
        obj.transform.SetSiblingIndex(1);
    }

}
