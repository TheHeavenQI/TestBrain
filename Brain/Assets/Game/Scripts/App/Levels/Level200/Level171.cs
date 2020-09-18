
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level171 : LevelBasePage {
    public Sprite tipImage;
    private bool _swipe1Open;
    private bool _swipe2Open;
    
    private bool _error;
    public Sprite image1;
    public Sprite image2;

    public Button okBtn;
    
    public List<EventCallBack> allSwipe;
    public List<Image> allSwipeImage = new List<Image>();
    protected override void Start() {
        base.Start();
        Global.tipSprite = tipImage;
        
        for (int i = 0; i < allSwipe.Count; i++) {
            var swipe = allSwipe[i];
            var im = swipe.transform.Find("Image").GetComponent<Image>();
            allSwipeImage.Add(im);
            var k = i;
            swipe.onClick = () => {
                if (swipe.name == "success1") {
                    _swipe1Open = true;
                } else if (swipe.name == "success2") {
                    _swipe2Open = true;
                } else {
                    _error = true;
                }
                allSwipeImage[k].sprite = image2;
            };
            swipe.onSwipe = (a) => {
                if (swipe.name == "success1") {
                    _swipe1Open = true;
                }else if (swipe.name == "success2") {
                    _swipe2Open = true;
                }
                else {
                    _error = true;
                }
                allSwipeImage[k].sprite = image2;
            };
        }
        
        okBtn.onClick.AddListener(() => {
            if (_swipe1Open && _swipe2Open && !_error) {
                Completion();
            }
            else {
                ShowError();
                Refresh();
            }
        });
    }
    
    public override void Refresh() {
        base.Refresh();
        _error = false;
        _swipe1Open = false;
        _swipe2Open = false;
        for (int i = 0; i < allSwipeImage.Count; i++) {
            allSwipeImage[i].sprite = image1;
        }
    }
}
