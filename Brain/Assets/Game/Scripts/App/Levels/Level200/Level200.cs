using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level200 : LevelBasePage {

    public RectTransform snail;
    public GameObject cup;
    public RectTransform[] bricks;
    public Button leftBtn;
    public Button rightBtn;

    public Image suc_1;
    public Image suc_2;
    private readonly List<RectTransform> _allTrans = new List<RectTransform>();
    private readonly List<Vector3> _allOriginPos = new List<Vector3>();

    private readonly HashSet<int> _swapSteps = new HashSet<int> { 1, 2, 3, 4 };
    private readonly int _maxStep = 7;
    private readonly float _moveSpeed = 90.857f;
    private readonly float _moveDuration = 0.3f;

    private int _step = 0;
    private bool _moving;

    protected override void Start()
    {
        base.Start();

        delayComplete = 0;

        _allTrans.Add(snail);
        _allTrans.AddRange(bricks);
        _allTrans.Add(leftBtn.image.rectTransform);
        _allTrans.Add(rightBtn.image.rectTransform);

        foreach (RectTransform transform in _allTrans)
        {
            _allOriginPos.Add(transform.position);
        }

        leftBtn.onClick.AddListener(() =>
        {
            if (_moving)
            {
                return;
            }
            _moving = true;

            snail.DOLocalMoveX(snail.localPosition.x - _moveSpeed, _moveDuration).OnComplete(() =>
            {
                snail.DOLocalMoveY(-900, _moveDuration).OnComplete(() =>
                {
                    ShowError();
                });
            });
        });
        rightBtn.onClick.AddListener(() =>
        {
            if (_moving)
            {
                return;
            }
            _moving = true;
            ++_step;
            if (_swapSteps.Contains(_step))
            {
                SwapBtnPos();
            }
            snail.DOLocalMoveX(snail.localPosition.x + _moveSpeed, _moveDuration).OnComplete(() =>
            {

                if (_step >= _maxStep)
                {
                    //Completion(snail.localPosition);
                    StartCoroutine("mCompletion");
                }
                else
                {
                    _moving = false;
                }
                bricks[_step - 1].DOLocalMoveY(-900, 0.3f);
            });
        });
    }
    IEnumerator mCompletion()
    {
        snail.gameObject.SetActive(false);
        cup.SetActive(false);
        suc_1.enabled = true;
        for (int i = 0; i < 140; ++i)
        {
            if (i % 35 == 0)
            {
                suc_1.enabled = i % 2 == 1;
                suc_2.enabled = i % 2 != 1;
            }
            yield return null;
        }
        Completion();
    }
    public override void Refresh() {
        base.Refresh();
        StopCoroutine("mCompletion");
        snail.gameObject.SetActive(true);
        cup.SetActive(true);
        suc_1.enabled = false;
        suc_2.enabled = false;
        for (int i = 0; i < _allTrans.Count; ++i) {
            DOTween.Kill(_allTrans[i]);
            _allTrans[i].position = _allOriginPos[i];
        }

        _step = 0;
        _moving = false;
    }

    private void SwapBtnPos() {
        Vector3 leftPos = leftBtn.transform.position;
        leftBtn.transform.position = rightBtn.transform.position;
        rightBtn.transform.position = leftPos;
    }
}
