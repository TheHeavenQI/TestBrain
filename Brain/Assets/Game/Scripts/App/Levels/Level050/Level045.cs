using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level045 : LevelBasePage {
   public List<Button> btns;
   private List<Image> _circles = new List<Image>();
   public GameObject wuyun;
   public Button yun;
   public List<GameObject> shuidi;
   private bool _showShuiDiAnim;
   public Transform qiuyinTransform;
   private Tweener qiuyinTweener;
   private Vector3 qiuyinVector;
   protected override void Start() {
      base.Start();
      qiuyinVector = qiuyinTransform.localPosition;
      shuidi = shuidi.RandomList();
      for (int i = 0; i < btns.Count; i++) {
         var im = btns[i].transform.Find("circle").GetComponent<Image>();
         im.gameObject.SetActive(false);
         _circles.Add(im);
         btns[i].onClick.AddListener(() => {
            im.gameObject.SetActive(true);
            CheckFinish();
         });
      }
      wuyun.SetActive(false);
      yun.onClick.AddListener(() => {
         wuyun.SetActive(true);
         ShowShuiDi();
         After(() => {
            ShowQiuYin();
         },1f);
      });
     
      for (int i = 0; i < shuidi.Count; i++) {
         shuidi[i].SetActive(false);
      }
   }

   private void CheckFinish() {
      bool isFinish = true;
      for (int i = 0; i < _circles.Count; i++) {
         if (!_circles[i].gameObject.activeInHierarchy) {
            isFinish = false;
            break;
         }
      }
      
      if (isFinish) {
         Completion();
      }
   }

   private void ShowQiuYin() {
      if (qiuyinTweener == null) {
         qiuyinTweener = qiuyinTransform.DOLocalMoveY(150+qiuyinTransform.localPosition.y, 1f);
      }
   }
   private void ShowShuiDi() {
      _showShuiDiAnim = true;
      for (int i = 0; i < shuidi.Count; i++) {
         var a = shuidi[i];
         After(() => {
               if (_showShuiDiAnim) {
                  a.SetActive(true);
               }
            },i*0.1f);
      }
   }
    public override void Refresh()
    {
        base.Refresh();
        wuyun.SetActive(false);
        _showShuiDiAnim = false;
        qiuyinTweener?.Kill();
        qiuyinTweener = null;
        shuidi = shuidi.RandomList();
        qiuyinTransform.localPosition = qiuyinVector;
        for (int i = 0; i < shuidi.Count; i++)
        {
            shuidi[i].SetActive(false);
        }
        foreach (var btn in btns)
        {
            btn.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
