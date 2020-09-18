using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Balloon_ : DragMove {

    private Tweener _tweener;
    private bool _isAnim;
    private Vector3 _orgLoc;
    private float _process;
    private int dir = 1;
    public float animDistance;
    public float animTime = 3;
    private Vector3 _startPos,_endPos;

    protected override void Start() {
        base.Start();
        _orgLoc = transform.localPosition;
        _startPos = _orgLoc - new Vector3(0,animDistance/2.0f,0);
        _endPos = _orgLoc + new Vector3(0,animDistance/2.0f,0);
        onDragEnd = () => {
            _isAnim = true;
            _tweener = transform.DOLocalMove(_orgLoc, 0.5f).OnComplete(() => { _isAnim = false; });
             Return2OriginPos(0.5f);
        };
        _process = 0.5f;
    }

    public void Refresh() {
        dir = 1;
         _process = 0.5f;
        _tweener.Kill();
        transform.localPosition = _orgLoc;
    }

    protected override  void Update() {
        if (!_isAnim && !isPressing) {
            if (_process >= 1) {
                dir = -1;
            }
            if (_process <= 0) {
                dir = 1;
            }
            _process += dir * Time.deltaTime / animTime;
            transform.localPosition = Vector3.Lerp(_startPos, _endPos, _process);
        }
    }
}
