using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level019 : LevelBasePage {
   public List<DragMove> dragMovesObjects;
   private List<Vector3> _locs = new List<Vector3>();
   protected override void Awake() {
      base.Awake();
      for (int i = 0; i < dragMovesObjects.Count; i++) {
         var k = i;
         var obj = dragMovesObjects[i];
         _locs.Add(obj.transform.localPosition);
         obj.onClick = () => { ShowErrorWithMousePosition(); };
         obj.onDragEnd = () => {
            var other = dragMovesObjects[k == 2 ? 3 : 2];
            if (Vector2.Distance(dragMovesObjects[2].transform.localPosition, dragMovesObjects[3].transform.localPosition) < 200) {
                 dragMovesObjects[3].transform.DOLocalMove(new Vector3(dragMovesObjects[2].transform.localPosition.x - 8, dragMovesObjects[2].transform.localPosition.y + 26, 0), 0.5f).OnComplete(() => {
                  Completion();
               });
            }
         };
      }
      Refresh();
   }
   
   public override void Refresh() {
      base.Refresh();
      _locs = ListExtend.RandomList(_locs);
      for (int i = 0; i < _locs.Count; i++) {
         dragMovesObjects[i].transform.localPosition = _locs[i];
      }
   }
   
}
