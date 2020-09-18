using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level248 : LevelBasePage
{
    public RectTransform tongsGroup;
    public RectTransform line;
    public RectTransform tongs;

    public DragMoveEventTrigger needle;
    public DragMoveEventTrigger head;
    public DragMoveEventTrigger body;
    public Button tongsBtn;

    private Image _needleImg;
    private Vector3 _tongsOriginPos;
    private Vector3 _lineOriginSize;
    private int _needleIndex;

    private enum TongsState
    {
        XMove,
        Down,
        Up
    }
    private TongsState _tongsState;
    private bool _isTongsLeftMove;
    private readonly float _tongXMoveSpeed = 75f;
    private readonly float _tongXMoveBound = 250;
    private readonly float _tongDownDuration = 0.5f;
    private readonly float _tongDownDistance = 280;
    private readonly float _tongGrapXOffset = 25;

    private enum NeedleState
    {
        Hide,
        Show,
        Rely
    }
    private NeedleState _needleState;

    private enum UpState
    {
        None,
        Error,
        Win
    }

    protected override void Start()
    {
        base.Start();

        _needleImg = needle.GetComponent<Image>();
        _needleImg.raycastTarget = true;
        
        _needleIndex = needle.transform.GetSiblingIndex();
        _tongsOriginPos = tongs.localPosition;
        _lineOriginSize = line.sizeDelta;

        body.enableDragMove = false;

        tongsBtn.onClick.AddListener(OnTongsBtnClick);

        needle.onBeginDrag = (d) => {
            needle.transform.SetAsLastSibling();
        };
        needle.onEndDrag = (d) => { 
            if (needle.rectTransform.IsRectTransformOverlap(head.rectTransform))
            {
                SetParent(needle.transform, head.rectTransform);
                _needleImg.raycastTarget = false;
                _needleState = NeedleState.Rely;
            }
            else
            {
                _needleState = NeedleState.Show;
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();

        tongsGroup.localPosition = Vector3.zero;
        tongs.DOKill();
        tongs.localPosition = _tongsOriginPos;
        line.DOKill();
        line.sizeDelta = _lineOriginSize;

        SetParent(needle.transform, head.transform.parent);
        needle.transform.SetSiblingIndex(_needleIndex);
        _needleImg.raycastTarget = true;
        needle.Return2OriginPos();

        head.DOKill();
        head.Return2OriginPos();

        SetParent(body.transform, head.transform);
        body.transform.SetAsFirstSibling();
        body.Return2OriginPos();

        _tongsState = TongsState.XMove;
        _needleState = NeedleState.Hide;
    }

    private void Update()
    {
        switch (_tongsState)
        {
            case TongsState.XMove:
                TongsXMove();
                break;
            default: 
                break;
        }
    }


    private void TongsXMove()
    {
        float offset = _tongXMoveSpeed * Time.deltaTime;
        offset *= _isTongsLeftMove ? -1 : 1;
        Vector3 pos = tongsGroup.localPosition;
        pos.x += offset;
        tongsGroup.localPosition = pos;

        if (pos.x < -_tongXMoveBound || pos.x > _tongXMoveBound)
        {
            _isTongsLeftMove = !_isTongsLeftMove;
        }
    }

    private void OnTongsBtnClick()
    {
        if (_tongsState != TongsState.XMove)
        {
            return;
        }
        TongsDown();
    }

    private void TongsDown()
    {
        _tongsState = TongsState.Down;

        tongs.DOLocalMoveY(_tongsOriginPos.y - _tongDownDistance, _tongDownDuration)
            .SetEase(Ease.Linear)
            .OnComplete(TongsUp);

        Vector2 lineSize = _lineOriginSize;
        lineSize.y += _tongDownDistance;
        line.DOSizeDelta(lineSize, _tongDownDuration).SetEase(Ease.Linear);
    }

    private void TongsUp()
    {
        _tongsState = TongsState.Up;
        UpState upState = UpState.None;

        line.DOSizeDelta(_lineOriginSize, _tongDownDuration).SetEase(Ease.Linear);
        tongs.DOLocalMoveY(_tongsOriginPos.y, _tongDownDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                switch (upState)
                {
                    case UpState.Win: 
                        Completion(); 
                        break;
                    case UpState.Error:
                        ShowError();
                        After(Refresh, 0.3f);
                        break;
                    case UpState.None:
                        _tongsState = TongsState.XMove;
                        break;
                }
            });

        if (
            //tongs.IsRectTransformOverlap(head.rectTransform)
            //&& 
            Mathf.Abs(tongsGroup.localPosition.x - head.rectTransform.localPosition.x) <= _tongGrapXOffset)
        {
            if (_needleState == NeedleState.Rely)
            {
                upState = UpState.Win;
            }
            else
            {
                SetParent(body.transform, head.transform.parent);
                body.transform.SetSiblingIndex(head.transform.GetSiblingIndex());

                upState = UpState.Error;
            }
            head.transform.DOLocalMoveY(head.transform.localPosition.y + _tongDownDistance, _tongDownDuration)
                .SetEase(Ease.Linear);
        }
    }

    private void SetParent(Transform trans, Transform parent)
    {
        Vector3 pos = trans.position;
        trans.transform.SetParent(parent, false);
        trans.position = pos;
    }
}
