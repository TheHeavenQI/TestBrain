using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TipsPopUp : BasePopUp
{
    public Image tipImage;
    public Text tipText;
    public Button okButton;
    public ContentSizeFitter sizeFitter;
    public override void Awake() {
        base.Awake();
        okButton.onClick.AddListener(() => {
            Hide();
        });
    }
    
    public void SetLevel(int level) {
        tipImage.sprite = Global.tipSprite;
        tipImage.gameObject.SetActive(tipImage.sprite != null);
        if (tipImage.gameObject.activeSelf) {
            tipImage.SetNativeSize();
            showNative = false;
        }
        else {
            showNative = true;
        }
        var model = ConfigManager.Current().GetQuestionModel(level - 1);
        tipText.text = model.tip;
        StartCoroutine(SetLayoutVertical());
    }

    private IEnumerator SetLayoutVertical() {
        yield return 1;
        sizeFitter.SendMessage("SetDirty");
        sizeFitter?.SetLayoutVertical();
        yield return 1;
    }
    public override void Hide()
    {
        base.Hide();
        EventCenter.Broadcast(UtilsEventType.OnTipsDialogClose);
    } 
}
