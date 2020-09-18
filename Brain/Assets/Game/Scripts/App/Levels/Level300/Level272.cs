using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class Level272 : LevelBasePage
{
    public RectTransform[] downItems;
    public Image fingerImg;
    public Sprite fingerNormalSprite;
    public Sprite fingerHurtSprite;

    public RectTransform konckArea;
    public Button hammerBtn;
    public GameObject hammer2Go;

    private readonly int _downItemX = -150;
    private readonly int _downItemGap = 300;
    private readonly int _downOutX = -500;
    private readonly int _downSpeed = 200;
    private readonly int _downErrorY = -200;

    private readonly HashSet<int> _konckItems = new HashSet<int>();

    private bool _isDead;
    private bool _isKnocking;

    protected override void Start()
    {
        base.Start();
        hammer2Go.SetActive(false);
        InitDownItems();

        hammerBtn.onClick.AddListener(() => {
            if (_isKnocking || _isDead)
            {
                return;
            }
            _isKnocking = true;
            hammerBtn.transform.DOLocalRotate(new Vector3(0, 0, 35), 0.1f)
                .OnComplete(() => {
                    bool isKnocked = false;
                    for (int i = 0; i < downItems.Length; ++i)
                    {
                        if (konckArea.IsRectTransformOverlap(downItems[i]))
                        {
                            hammer2Go.SetActive(true);
                            if (downItems[i] == fingerImg.rectTransform)
                            {
                                _isDead = true;
                                fingerImg.sprite = fingerHurtSprite;
                                ShowError(fingerImg.rectTransform.localPosition);
                                After(Refresh, 0.5f);
                            }
                            else
                            {
                                downItems[i].DOLocalMoveX(_downOutX, 0.5f);
                                _konckItems.Add(i);
                                if (_konckItems.Count >= downItems.Length - 1)
                                {
                                    Completion();
                                }
                                After(() => {
                                    hammer2Go.SetActive(false);
                                    hammerBtn.transform.localEulerAngles = Vector3.zero;
                                    _isKnocking = false;
                                }, 0.1f);
                            }
                            isKnocked = true;
                        }
                    }
                    if (!isKnocked)
                    {
                        After(() => {
                            hammerBtn.transform.localEulerAngles = Vector3.zero;
                            _isKnocking = false;
                        }, 0.1f);
                    }
                });
        });
    }


    public override void Refresh()
    {
        base.Refresh();
        hammer2Go.SetActive(false);

        InitDownItems();
        _konckItems.Clear();
        hammerBtn.transform.DOKill();
        hammerBtn.transform.localEulerAngles = Vector3.zero;

        fingerImg.sprite = fingerNormalSprite;

        _isKnocking = false;
        _isDead = false;
    }

    private void Update()
    {
        if (_isDead || isLevelComplete)
        {
            return;
        }

        for (int i = 0; i < downItems.Length; ++i)
        {
            if (!_konckItems.Contains(i))
            {
                Vector3 pos = downItems[i].localPosition;
                pos.y -= _downSpeed * Time.deltaTime;
                downItems[i].localPosition = pos;
                if (downItems[i] != fingerImg.rectTransform && pos.y < _downErrorY)
                {
                    _isDead = true;
                    ShowError(downItems[i].localPosition);
                    After(Refresh, 0.5f);
                }
            }
        }
    }

    private void InitDownItems()
    {
        for (int i = 0; i < downItems.Length; ++i)
        {
            float y = 800 + _downItemGap * i;
            downItems[i].DOKill();
            downItems[i].localPosition = new Vector3(_downItemX, y);
        }
    }
}
