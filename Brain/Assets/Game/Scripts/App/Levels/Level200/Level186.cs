using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;

public class Level186 : SelectNumLevel {

    public Image headImage;
    public Sprite[] headSprites;
    public Image blackImage;

    private Coroutine _coroutine;
    private bool _isDoFace;
    private float _deltaDuration = 0.5f;

    protected override void Start() {
        base.Start();
        blackImage.transform.SetAsLastSibling();
        headImage.sprite = headSprites[0];
        headImage.SetNativeSize();
        headImage.GetComponent<Button>().onClick.AddListener(() => {
            if (_isDoFace) {
                return;
            }
            DoFace();
        });
    }

    private void DoFace() {
        _coroutine = this.Delay(_deltaDuration)
                        .Do(() => {
                            headImage.sprite = headSprites[1];
                            headImage.SetNativeSize();
                        })
                        .Delay(_deltaDuration)
                        .Do(() => {
                            headImage.sprite = headSprites[2];
                            headImage.SetNativeSize();
                        })
                        .Delay(_deltaDuration)
                        .Do(() => {
                            headImage.sprite = headSprites[3];
                            headImage.SetNativeSize();
                        })
                        .Delay(_deltaDuration)
                        .Do(() => {
                            blackImage.gameObject.SetActive(true);
                            headImage.sprite = headSprites[0];
                            headImage.SetNativeSize();
                        })
                        .Delay(_deltaDuration * 2)
                        .Do(() => {
                            blackImage.gameObject.SetActive(false);
                            _isDoFace = false;
                        })
                        .Execute()
                        .GetCoroutine();
    }

    public override void Refresh() {
        base.Refresh();

        if (_coroutine != null) {
            this.StopCoroutine(_coroutine);
        }
        blackImage.gameObject.SetActive(false);
        headImage.sprite = headSprites[0];
        headImage.SetNativeSize();
    }

    protected override void OnCompletion() {
        base.OnCompletion();
        if (_coroutine != null) {
            this.StopCoroutine(_coroutine);
        }
    }
}
