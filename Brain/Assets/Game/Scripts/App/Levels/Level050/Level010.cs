using UnityEngine;

public class Level010 : LevelBasePage {

    public GameObject mao1;
    public GameObject mao2;
    public DragMove sunDragMove;
    public GameObject mask;
    private Vector3 _sunLoc;
    protected override void Start() {
        base.Start();
        _sunLoc = sunDragMove.transform.localPosition;
        var sunRect =  sunDragMove.transform.GetComponent<RectTransform>();
        var parentRect = transform.GetComponent<RectTransform>();
        sunDragMove.onDragEnd = () => {
            if (sunRect.localPosition.y < -parentRect.rect.height*0.5f + sunRect.sizeDelta.y*0.5f) {
                mask.SetActive(true);
                SetEnableClick(false);

                Invoke("Dismiss", 0f);
                Invoke("Show", 0.2f);
                Invoke("Dismiss", 0.4f);
                Invoke("Show", 0.6f);

                After(() => {
                    mask.SetActive(false);
                    Completion();
                },1);
            }
        };
        Refresh();
    }
    
    public override void Refresh() {
        base.Refresh();
        sunDragMove.transform.localPosition = _sunLoc;
        Dismiss();
        mask.SetActive(false);
    }

    void Show()
    {
        mao1.SetActive(false);
        mao2.SetActive(true);
    }

    void Dismiss()
    {
        mao1.SetActive(true);
        mao2.SetActive(false);
    }

}
