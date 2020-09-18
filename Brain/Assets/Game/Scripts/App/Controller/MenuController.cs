
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : BaseController {
    public Button bgSoundButton;
    public Button soundButton;
    public Button vibrationButton;
  
    private Image _bgSoundImage;
    private Image _soundImage;
    private Image _vibrationImage;
    public List<Sprite> settingSprite;
    public override void Awake() {
        base.Awake();
        _bgSoundImage = bgSoundButton.transform.Find("Image").GetComponent<Image>();
        _soundImage = soundButton.transform.Find("Image").GetComponent<Image>();
        _vibrationImage =vibrationButton.transform.Find("Image").GetComponent<Image>();
        
        transform.Find("Content/Daily").GetComponent<Button>().onClick.AddListener(DailyAction);
        transform.Find("Content/Scroe").GetComponent<Button>().onClick.AddListener(ScroeAction);
        transform.Find("Content/Feedback").GetComponent<Button>().onClick.AddListener(FeedbackAction);
        transform.Find("Content/Terms").GetComponent<Button>().onClick.AddListener(TermsAction);
        transform.Find("Content/Privacy").GetComponent<Button>().onClick.AddListener(PrivacyAction);

        var Language = transform.Find("Content/Language");
        Language.GetComponent<Button>().onClick.AddListener(LanguageSetting);
        EventCenter.AddListener(UtilsEventType.LanguageSwitch,LanguageSwitch);
        
        bgSoundButton.onClick.AddListener(() => {
            var user = UserModel.Get();
            user.bgSound = !user.bgSound;
            UserModel.Save(user);
            _bgSoundImage.sprite = settingSprite[user.bgSound?0:1];
            _bgSoundImage.SetNativeSize();
            EventCenter.Broadcast<bool>(UtilsEventType.BgSoundSwitch,user.bgSound);
        });
        soundButton.onClick.AddListener(() => {
            var user = UserModel.Get();
            user.sound = !user.sound;
            UserModel.Save(user);
            _soundImage.sprite = settingSprite[user.sound?2:3];
            _soundImage.SetNativeSize();
        });
        vibrationButton.onClick.AddListener(() => {
            var user = UserModel.Get();
            user.vibration = !user.vibration;
            UserModel.Save(user);
            _vibrationImage.sprite = settingSprite[user.vibration?4:5];
            _vibrationImage.SetNativeSize();
        });
    }

    public override void Start() {
        base.Start();
        var user = UserModel.Get();
        _bgSoundImage.sprite = settingSprite[user.bgSound?0:1];
        _soundImage.sprite = settingSprite[user.sound?2:3];
        _vibrationImage.sprite = settingSprite[user.vibration?4:5];
        _bgSoundImage.SetNativeSize();
        _soundImage.SetNativeSize();
        _vibrationImage.SetNativeSize();
    }
    

    private void LanguageSetting() {
        ControllerManager.Instance.GetController<LanguageController>().gameObject.SetActive(true);
    }
    private void DailyAction() {
        QiaoDaoController ctr = ControllerManager.Instance.GetController<QiaoDaoController>();
        ctr.Init();
        ctr.gameObject.SetActive(true);
    }
    private void ScroeAction() {
        PopUpManager.Instance.ShowScore(false,null);
    }
    private void FeedbackAction() {
        PopUpManager.Instance.ShowScore(true,null);
    }

    private void TermsAction() {
        
    }
    private void PrivacyAction() {
        
    }
}
