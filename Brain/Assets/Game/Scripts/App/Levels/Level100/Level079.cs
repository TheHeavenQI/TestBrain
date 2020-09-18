using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level079 : LevelBasePage
{
    public Button photo;
    public EventCallBack xinfeng;
    private Vector3 _photoPos;
    private Vector3 _xinfenPos;
    private bool _isDown;
    protected override void Start()
    {
        base.Start();
        _photoPos = photo.transform.localPosition;
        _xinfenPos = xinfeng.transform.localPosition;
        xinfeng.onClick = () => {
            ShowError();
        };
        photo.onClick.AddListener(() => {
            Completion();
        });
    }

    public override void Refresh()
    {
        base.Refresh();
        _isDown = false;
        xinfeng.transform.localPosition = _xinfenPos;
        photo.transform.localPosition = _photoPos;
        photo.gameObject.SetActive(false);
    }

    private void Update()
    {
        DeviceOrientation orient = Input.deviceOrientation;

        if (orient == DeviceOrientation.PortraitUpsideDown || Input.GetKeyDown(KeyCode.Q))
        {
            if (_isDown)
            {
                return;
            }
            _isDown = true;

            DOTween.Kill(photo.transform);
            DOTween.Kill(xinfeng.transform);
            photo.gameObject.SetActive(true);
            photo.transform.DOLocalMove(photo.transform.localPosition + new Vector3(0, 450, 0), 0.5f);
            if (!xinfeng.isPressing)
            {
                xinfeng.transform.DOLocalMove(xinfeng.transform.localPosition + new Vector3(0, 450, 0), 0.5f);
            }
        }
        else if (orient == DeviceOrientation.Portrait || Input.GetKeyDown(KeyCode.W))
        {
            if (!_isDown)
            {
                return;
            }
            _isDown = false;

            DOTween.Kill(photo.transform);
            DOTween.Kill(xinfeng.transform);
            photo.transform.DOLocalMove(_photoPos, 0.5f);
            if (!xinfeng.isPressing)
            {
                xinfeng.transform.DOLocalMove(_xinfenPos, 0.5f);
            }
        }
    }
}
