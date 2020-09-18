using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Level112 : LevelBasePage {

    public CustomEventTrigger box;
    public Button candyBtn;

    private bool _isBoxPress;

    private bool _isCandyMove;
    private bool _isBoxMove;

    private Vector3 _boxOriginPos;
    private Vector3 _candyOriginPos;

    protected override void Start() {
        base.Start();

        box.onPointerDown += (d) => _isBoxPress = true;
        box.onPointerUp += (d) => _isBoxPress = false;

        candyBtn.onClick.AddListener(() => {
            CompletionWithMousePosition();
        });

        _boxOriginPos = box.transform.localPosition;
        _candyOriginPos = candyBtn.transform.localPosition;

        candyBtn.image.enabled = false;
    }

    private void Update() {

        DeviceOrientation orient = Input.deviceOrientation;

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space)) {
#else
        if (orient == DeviceOrientation.PortraitUpsideDown) {
#endif
            if (_isBoxPress) {
                if (!_isCandyMove) {
                    _isCandyMove = true;

                    candyBtn.image.DOFade(0, 0.3f).From();
                    candyBtn.transform.DOLocalMoveY(200, 1).OnComplete(() => {
                        candyBtn.image.raycastTarget = true;
                    });
                    candyBtn.image.enabled = true;
                }
            } else {
                if (!_isBoxMove && !_isCandyMove) {
                    _isBoxMove = true;
                    box.transform.DOLocalMoveY(1100, 1.5f)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => {
                            ShowError();
                            Refresh();
                        });
                }
            }
        }
    }

    public override void Refresh() {
        base.Refresh();

        _isCandyMove = false;
        _isBoxMove = false;
        _isBoxPress = false;

        candyBtn.image.enabled = false;

        box.transform.localPosition = _boxOriginPos;
        candyBtn.transform.localPosition = _candyOriginPos;
    }
}
