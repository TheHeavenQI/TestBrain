using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level043 : LevelBasePage {
    public Image[] treesDark;
    public Image[] tressLight;


    public EventCallBack dragMove1;
    public EventCallBack dragMove2;
    protected override void Start() {
        base.Start();

        //dragMove2.needPressTime = 1f;
        dragMove2.onPointerDown = () => {
            if (dragMove1.isPressing) {
                foreach (Image image in treesDark) {
                    image.gameObject.SetActive(false);
                }
                foreach (Image image in tressLight) {
                    image.gameObject.SetActive(true);
                }

                Completion();
            }
        };

        //dragMove1.needPressTime = 1f;
        dragMove1.onPointerDown = () => {
            if (dragMove2.isPressing) {
                foreach (Image image in treesDark) {
                    image.gameObject.SetActive(false);
                }
                foreach (Image image in tressLight) {
                    image.gameObject.SetActive(true);
                }

                Completion();
            }
        };

    }
}
