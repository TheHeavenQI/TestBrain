using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level017 : LevelBasePage {
   public DragMove bird;
   public Transform answerPos;
   private Vector3 _orgVector3;
   public Transform water;
   protected override void Awake() {
      base.Awake();
      _orgVector3 = bird.transform.localPosition;
      var waterRect = water.GetComponent<RectTransform>();
      bird.onDragEnd = () => {
          var a = bird.transform.GetComponent<RectTransform>();
          if (RectTransformExtensions.IsRectTransformOverlap(a, waterRect)) {
              bird.transform.DOLocalMove(answerPos.localPosition, 0.5f).OnComplete(() => {
                  Completion();
              });
          }
          else {
              bird.transform.DOLocalMove(_orgVector3, 0.5f).OnComplete(() => {
                  ShowError();
              });
          }
      };
   }
}
