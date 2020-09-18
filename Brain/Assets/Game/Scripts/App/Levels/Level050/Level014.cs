using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level014 : LevelBasePage
{
    public GameObject chick;
    public List<GameObject> chickens;
    private List<Vector3> poslist = new List<Vector3>();
    private float _dragTime;
    private float _lastDragTime;

    private bool _showChick;
    public bool showChick {
        set {
            _showChick = value;
            chick.SetActive(value);
        }
        get { return _showChick; }
    }
    protected override void Start() {
        base.Start();
        for (int i = 0; i < chickens.Count; i++) {
            int j = i;
            poslist.Add(chickens[j].transform.localPosition);

            DragMove dragmove = chickens[j].GetComponent<DragMove>();
            dragmove.onClick = () => { ShowErrorWithMousePosition(); };
            dragmove.needPressTime = 0.5f;
            dragmove.onLongPress = () => {
                chickens[j].transform.DOShakeRotation(1.0f, 50);
            };
        }
        chick.transform.localPosition = chickens[0].transform.localPosition;
        chick.GetComponent<DragMove>().onClick = () => {
            CompletionWithMousePosition();
        };
        showChick = false;
        DragMove dragMove = chickens[0].GetComponent<DragMove>();
        
        dragMove.onDrag = () => {
            if (showChick == false) {
                var time = Time.realtimeSinceStartup;
                if (time - _lastDragTime < 1) {
                    _dragTime += time - _lastDragTime;
                }
                else {
                    _dragTime = 0;
                }
                _lastDragTime = time;
                if (_dragTime > 1f) {
                    showChick = true;
                    float x = chick.transform.localPosition.x;
                    float y = chick.transform.localPosition.y;
                    x += x > 0 ? (-50) : 50;
                    y += y > 0 ? (-50) : 50;
                    chick.transform.SetAsLastSibling();
                    chick.transform.localScale = new Vector3(0,0,0);
                    chick.transform.DOLocalMove(new Vector3(x, y, 0), 1).OnComplete(() => {
                        
                    });
                    chick.transform.DOScale(1, 1);
                }
                chick.transform.localPosition = dragMove.transform.localPosition;
            }
        };
    }
    
    public override void Refresh() {
        base.Refresh();
        poslist = poslist.RandomList();
        for (int i = 0; i < chickens.Count; i++) {
            chickens[i].transform.localPosition = poslist[i];
        }
        showChick = false;
        chick.transform.localPosition = chickens[0].transform.localPosition;
    }
}
