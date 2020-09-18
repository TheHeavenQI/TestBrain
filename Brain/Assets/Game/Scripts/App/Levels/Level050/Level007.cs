using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class Level007 : LevelBasePage {
   public List<GameObject> btnList;
   private List<Vector3> postions = new List<Vector3>();
   private List<Vector3> scales = new List<Vector3>();
   protected override void Start() {
      base.Start();
      for (int i = 0; i < btnList.Count; i++) {
         GameObject btn = btnList[i];
         postions.Add(btn.transform.localPosition);
         var a = btn.GetComponent<DragMove>();
         a.onDragEnd = () => { OnDragEnd(btn); };
         a.onClick = () => {
            int showCount = 0;
            for (int j = 0; j < btnList.Count; j++) {
               if (btnList[j].gameObject.activeInHierarchy) {
                  showCount++;
               }
            }

            if (showCount == 1) {
               CompletionWithMousePosition();
            }
            else {
               ShowErrorWithMousePosition();
            }
            
         };
         scales.Add(btn.transform.localScale);
      }
   }
   
   private void OnDragEnd(GameObject button) {
      for (int i = 0; i < btnList.Count; i++) {
         GameObject btn = btnList[i];
         if (btn != button && btn.gameObject.activeInHierarchy) {
            var dis =  Vector2.Distance(button.transform.localPosition, btn.transform.localPosition);
            float scale1 = button.transform.localScale.x;
            float scale2 = btn.transform.localScale.x;
            if (dis < 80*(scale1+scale2)) {
               if (scale1 > scale2) {
                  var scale = scale1 + scale2 / 3.0f;
                  button.transform.DOScale(new Vector3(scale,scale,0),0.5f);
                  btn.gameObject.SetActive(false);
               }
               else {
                  var scale = scale2 + scale1 / 3.0f;
                  button.gameObject.SetActive(false);
                  btn.transform.DOScale(new Vector3(scale,scale,0),0.5f);
               }
               return;
            }
         }
      }
   }
   
   public override void Refresh() {
      base.Refresh();
      for (int i = 0; i < btnList.Count; i++) {
         btnList[i].SetActive(true);
         btnList[i].transform.localScale = scales[i];
         btnList[i].transform.localPosition = postions[i];
      } 
   }
}
