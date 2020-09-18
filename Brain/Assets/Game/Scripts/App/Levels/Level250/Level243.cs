
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level243 : LevelBasePage
{
    private float _speed = 5;

    public Sprite sunSprite;
    public Sprite moonSprite;
    public Sprite renSprite;
    public Sprite langSprite;
    public Image renImage;
    public Image sunImage;
    public Transform clockTransform;
    public EventCallBack clockEventObject;
    public Button button;
    public Button imageButton;
    private bool _isSun = false;
    private bool _isAnim = false;
    private void SetSun(bool sun)
    {
        _isSun = sun;
        sunImage.sprite = sun?sunSprite : moonSprite;
        renImage.sprite = sun?renSprite : langSprite;
        renImage.SetNativeSize();
        sunImage.SetNativeSize();
    }
    protected override void Start()
    {
        base.Start();
       
        clockEventObject.onDragDraging = (pos) =>
        {
            if (_isAnim)
            {
                return;
            }
            _isAnim = true;
            clockEventObject.transform.DORotate(new Vector3(0,0, _isSun?180:0), 1).OnComplete(()=>
            {
                _isAnim = false;
                SetSun(!_isSun);
            });
        };
        button.onClick.AddListener(() =>
        {
            if (!_isSun)
            {
                CompletionWithMousePosition();
            }
            else
            {
                ShowErrorWithMousePosition();
            }
        });
        imageButton.onClick.AddListener(() =>
        {
            ShowErrorWithMousePosition();
        });
        
    }
    public override void Refresh()
    {
        base.Refresh();
        SetSun(true);
        clockEventObject.transform.localEulerAngles = Vector3.zero;
    }

}
