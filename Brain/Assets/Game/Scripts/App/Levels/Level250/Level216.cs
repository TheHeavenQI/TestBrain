
using UnityEngine;
using UnityEngine.UI;

public class Level216 : LevelBasePage {
    public Sprite image1;
    public Sprite image2;
    public Sprite imageOk;
    public Transform lineTransform;
    public Image yangImage;
    public Button leftButton;
    public Button rightButton;
    public DragMove san;
    private float _sanx;
    protected override void Start() {
        base.Start();
        _sanx = san.transform.localPosition.x;
        san.onDragEnd = () => {
            After(() => {
                yangImage.sprite = imageOk;
                lineTransform.rotation = Quaternion.Euler(0,0,0);
                Completion();
            },1);
        };
        leftButton.onClick.AddListener(() => {
            yangImage.transform.localPosition += new Vector3(-10,0,0);
            Rotation();
        });
        rightButton.onClick.AddListener(() => {
            yangImage.transform.localPosition += new Vector3(10,0,0);
            Rotation();
        });
        Rotation();
    }

    private void Rotation() {
        float x1 = yangImage.transform.localPosition.x;
        float diff = _sanx - x1;
        if (diff > -100 && diff < 100) {
            float ra = diff / 10;
            float mm = 2;
            if (ra > -mm && ra < mm) {
                ra = ra > 0 ? mm: -mm;
            }
            if (ra > 0) {
                yangImage.sprite = image2;
            }
            else {
                yangImage.sprite = image1;
            }
            lineTransform.rotation = Quaternion.Euler(0,0,ra);
        }
        else {
            ShowError();
            After(() => {
                Refresh();
            },0.5f);
        }
    }

    public override void Refresh() {
        base.Refresh();
        yangImage.sprite = image1;
        san.Return2OriginPos();
        yangImage.transform.localPosition = new Vector3(_sanx,yangImage.transform.localPosition.y,0);
        Rotation();
    }
}
