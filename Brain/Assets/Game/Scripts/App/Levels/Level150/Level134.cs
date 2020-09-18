using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level134 : LevelBasePage {

    public Button[] btns;
    public Transform fire;
    private Vector3 _fireVector3;
    private bool _finished;
    public EventCallBack eventCallBack;
    public Transform obj;
    private Vector3 _firstPos;
    private List<Vector3> posList = new List<Vector3>();
    protected override void Start() {
        base.Start();
        _firstPos = obj.localPosition;
        _fireVector3 = fire.localPosition;
        foreach (Button btn in btns) {
            btn.onClick.AddListener(() => ShowErrorWithMousePosition());
            btn.GetComponent<DragMove>().onDragEnd = () =>
            {
                btn.GetComponent<DragMove>().Return2OriginPos();
                ShowErrorWithMousePosition();
            };
        }
        eventCallBack.onDragDragingWithOutOffset = (a) => {
            obj.position = a;
            AddPoint(obj.localPosition);
        };
        eventCallBack.onDragEnd = () => { posList = new List<Vector3>();};
        eventCallBack.transform.SetAsLastSibling();
    }

    private void Finish() {
        if (_finished) {
            return;
        }
        _finished = true;
        fire.DOLocalMove(_fireVector3 + new Vector3(10, -300, 0), 1).OnComplete(() => { Completion(); });
        fire.DORotate(new Vector3(0,0,-180),1);
    }

    public override void Refresh() {
        base.Refresh();
        _finished = false;
        fire.localEulerAngles = Vector3.zero;
        fire.localPosition = _fireVector3;
    }

    private void AddPoint(Vector3 pos) {
        if (posList.Count == 0) {
            posList.Add(pos);
        }
        else {
            Vector3 last = posList[posList.Count - 1];
            int cnt = (int) (Vector2.Distance(pos, last) / 15) + 1;
            for (int i = 1; i < cnt; i++) {
                Vector3 tmp = Vector3.Lerp(last, pos, i/(float)cnt);
                posList.Add(tmp);   
            }
        }
        
        for (int i = 0; i < posList.Count; i++) {
            Vector3 tmp = posList[i];
            if (Vector2.Distance(tmp, _firstPos) < 30) {
                Finish();
            }
        }
    }
    
    
}
