using UnityEngine;
using UnityEngine.UI;

public class Level130 : LevelBasePage {

    public Button[] buttons;
    public Button boyBtn;
    public DragMove hairDM;
    private bool _isHairOut;
    private Image _boyImage;
    public Sprite sel_Sprite1;
    public Sprite sel_Sprite2;
    Vector3 manHairDefPos;
    protected override void Start() {
        base.Start();
        manHairDefPos = hairDM.transform.localPosition;
        _boyImage = boyBtn.GetComponent<Image>();
        for (int i = 0; i < buttons.Length; ++i) {
            int k = i;
            buttons[k].onClick.AddListener(() => {
                if (buttons[k] == boyBtn && _isHairOut) {
                    CompletionWithMousePosition();
                    return;
                }
                ShowErrorWithMousePosition();
            });
        }

        hairDM.onDragEnd = () => {
            _isHairOut = Vector3.Distance(hairDM.transform.localPosition, manHairDefPos) > 30;
            //!RectTransformExtensions.IsRectTransformOverlap(hairDM.rectTransform, boyBtn.transform as RectTransform);
            if (_isHairOut) {
                _boyImage.sprite = sel_Sprite2;
            }
            else {
                _boyImage.sprite = sel_Sprite1;
            }
            _boyImage.SetNativeSize();
        };
    }

    public override void Refresh() {
        base.Refresh();
        hairDM.Return2OriginPos();
        _isHairOut = false;
        _boyImage.sprite = sel_Sprite1;
        _boyImage.SetNativeSize();
    }
}
