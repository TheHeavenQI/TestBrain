
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level155 : LevelBasePage {
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Transform luobo;
    public AnimalMove tuzi;
    private Transform _tuziTransform;
    public List<DragMove> stars;
    private bool _zoomUp;
    
    public Vector2 jumpDownOffset { set; get; } = new Vector2(100, -200);
    public Vector2 jumpUpOffset { set; get; } = new Vector2(100, 200);
    
    protected override void Start() {
        base.Start();
        _tuziTransform = tuzi.transform.Find("Image");
        tuzi.CheckDown();
        leftButton.onClick.AddListener(() => {
            tuzi.Moveto(new Vector2(-10,0));
            if (Vector2.Distance(tuzi.transform.localPosition,luobo.localPosition) < 150) {
                Completion();
            }
        });
        rightButton.onClick.AddListener(() => {
            tuzi.Moveto(new Vector2(10,0));
            if (Vector2.Distance(tuzi.transform.localPosition,luobo.localPosition) < 150) {
                Completion();
            }
        });
        jumpButton.onClick.AddListener(() => {
            tuzi.JumpUp();
        });
        tuzi.errorAction = () => {
            ShowError();
            Refresh();
        };
        tuzi.correctAction = () => { Completion(); };
        
        for (int i = 0; i < stars.Count; i++) {
            var btn = stars[i];
            btn.onDragEnd = () => {
                var tuziPos = tuzi.transform.localPosition + new Vector3(0, -80, 0);
                if (!_zoomUp && Vector2.Distance(btn.transform.localPosition,tuziPos) < 200) {
                    _zoomUp = true;
                    btn.gameObject.SetActive(false);
                    tuzi.jumpDownOffset = jumpDownOffset*4;
                    tuzi.jumpUpOffset = jumpUpOffset*4;
                    _tuziTransform.localScale = new Vector3(2,2,2);
                }
                else {
                    btn.Return2OriginPos(0.5f);
                }
            };
        }
    }

    public override void Refresh() {
        base.Refresh();
        _zoomUp = false;
        tuzi.jumpDownOffset = jumpDownOffset;
        tuzi.jumpUpOffset = jumpUpOffset;
        _tuziTransform.localScale = new Vector3(1,1,1);
        tuzi.Refresh();
        for (int i = 0; i < stars.Count; i++) {
            stars[i].gameObject.SetActive(true);
            stars[i].Return2OriginPos();
        }
    }
    
}
