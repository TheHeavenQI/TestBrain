using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level192 : LevelBasePage {

    public GameObject lightOnGo;
    public GameObject lightOffGo;

    private float _faceDownStartTime = -1;
    private bool _isFaceDown;
    private readonly float _needFaceDownTime = 2;
    private bool _isReadyCompletion;

    protected override void Start() {
        base.Start();
        lightOffGo.SetActive(false);
    }

    private void Update() {
        if (isLevelComplete) {
            return;
        }

        DeviceOrientation orient = Input.deviceOrientation;

        if (_isReadyCompletion) {
            if (orient == DeviceOrientation.FaceUp) {
                Completion();
            }
            return;
        }

        if (!_isFaceDown) {
            if (orient == DeviceOrientation.FaceDown) {
                _isFaceDown = true;
                _faceDownStartTime = Time.time;
            }
        } else {
            if (orient == DeviceOrientation.FaceDown) {
                if (Time.time - _faceDownStartTime >= _needFaceDownTime) {
                    _isReadyCompletion = true;
                    lightOffGo.SetActive(true);
                    lightOnGo.SetActive(false);
                }
            } else {
                _isFaceDown = false;
            }
        }
    }
}
