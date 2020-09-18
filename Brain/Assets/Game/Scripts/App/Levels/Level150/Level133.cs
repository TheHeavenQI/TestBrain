
using DG.Tweening;
using UnityEngine;

public class Level133 : LevelBasePage {
    public Transform left;
    public Transform right;
    public Transform chen;
    public DragMove wenzi;
    public DragMove cat;
    public DragMove money;
    private int _leftCount;
    private int _rightCount;
    protected override void Start() {
        base.Start();
        AA(money);
        AA(cat);
        AA(wenzi);
        _leftCount = 0;
        _rightCount = 0;
        foreach (Transform sub in left.transform) {
            var num = int.Parse(sub.name);
            _leftCount += num;
        }
        foreach (Transform sub in right.transform) {
            var num = int.Parse(sub.name);
            _rightCount += num;
        }
        Check();
    }

    private void AA(DragMove move) {
        move.onDragBegin = () => {
            var num = int.Parse(move.name); 
            if (move.transform.parent.name == "left") {
                _leftCount -= num;
            }
            if (move.transform.parent.name == "right") {
                _rightCount -= num;
            }
            move.transform.SetParent(transform);
        };
        move.onDragEnd = () => {
            var num = int.Parse(move.name); 
            move.transform.SetParent(chen);
            move.transform.SetSiblingIndex(1);
            if (Vector3.Distance(move.transform.localPosition,left.localPosition) < 100) {
                move.transform.SetParent(left);
                _leftCount += num;
                move.transform.DOLocalMove(Vector3.zero, 0.5f);
            }else if (Vector3.Distance(move.transform.localPosition,right.localPosition) < 100) {
                _rightCount += num;
                move.transform.SetParent(right);
                move.transform.DOLocalMove(Vector3.zero, 0.5f);
            }
            else {
                move.transform.SetParent(transform);
            }
            Debug.Log($"_leftCount:{_leftCount} _rightCount:{_rightCount}");
            Check();
        };
    }

    private void Check() {
        if (_leftCount > _rightCount) {
            chen.transform.DORotate(new Vector3(0, 0, 30), 0.5f);
        }
        else if(_leftCount < _rightCount){
            chen.transform.DORotate(new Vector3(0, 0, -30), 0.5f);
        }
        else {
            chen.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
            After(() => {
                Completion();
            },1);
        }
    }
    private Vector3 GetLocationPos(Transform transform) {
        if (transform.parent.parent.name == "chen") {
            var a = transform.localPosition + transform.parent.localPosition + transform.parent.parent.localPosition;
            return a;
        }
        else {
            return transform.localPosition;

        }
    }
}
