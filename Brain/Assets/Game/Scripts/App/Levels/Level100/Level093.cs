
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level093 : LevelBasePage {
    public EventCallBack wanEventCallBack;
    public RectTransform wan;
    public DragMove mian;
    public DragMove shuihu;
    public Sprite shuihu1;
    public Sprite shuihu2;
    public Transform shuihuCorrect;
    public GameObject reqi;
    protected override void Start() {
        base.Start();
        reqi.SetActive(false);
        mian.onDragEnd = () => {
            if (Vector3.Distance(mian.transform.localPosition,wan.localPosition) < 200) {
                mian.transform.DOLocalMove(wan.localPosition, 0.5f);
            }
        };
        shuihu.onDragEnd = () => {
            if (Vector3.Distance(shuihu.transform.localPosition,wan.localPosition) < 200) {
                shuihu.transform.DOLocalMove(shuihuCorrect.localPosition, 0.5f).OnComplete(() => {
                    var im = shuihu.GetComponent<Image>();
                    im.sprite = shuihu2;
                    im.SetNativeSize();
                    After(() => {
                        wan.gameObject.SetActive(false);
                        mian.gameObject.SetActive(false);
                        im.sprite = shuihu1;
                        im.SetNativeSize();
                    },0.5f);
                });
            }
        };
        wanEventCallBack.onClick = () => {
            ShowErrorWithMousePosition();
        };
        wanEventCallBack.onLongPress = () => {
            reqi.SetActive(true);
            After(() => {
                CompletionWithMousePosition();
            },0.5f);
        };
        wanEventCallBack.needPressTime = 1;
    }
    
    public override void Refresh() {
        base.Refresh();
        mian.Return2OriginPos();
        shuihu.Return2OriginPos();
        var im = shuihu.GetComponent<Image>();
        im.sprite = shuihu1;
        im.SetNativeSize();
        reqi.SetActive(false);
        wan.gameObject.SetActive(true);
        mian.gameObject.SetActive(true);
    }
}
