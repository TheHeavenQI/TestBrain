
using UnityEngine;
using UnityEngine.UI;


public class Level124 : LevelBasePage {

    private float _lastUpTime;
    private bool _isPress;
    private readonly float _waitTime = 10;

    public Text numText;
    private float _sec;
    protected override void Start() {
        base.Start();
        _sec = -100;
        _lastUpTime = Time.time;
        numText.gameObject.SetActive(false);
    }
    
    private void Update() {
        if (isLevelComplete) {
            return;
        }

        if (Input.GetMouseButton(0)) {
            if (!_isPress) {
                _isPress = true;
            }
        } else {
            if (_isPress) {
                _isPress = false;
                _lastUpTime = Time.time;
            } else {
                float _secTmp = Time.time - _lastUpTime;
                if ((int)_sec != (int)_secTmp) {
                    int s = (int) (_waitTime - _sec);
                    if (s <= 7) {
                        numText.gameObject.SetActive(true);
                        numText.text = $"{s}";
                    }
                    else {
                        numText.gameObject.SetActive(false);
                    }
                }
                _sec = _secTmp;
                if (Time.time - _lastUpTime >= _waitTime) {
                    numText.gameObject.SetActive(false);
                    Completion();
                }
            }
        }
    }
    
    public override void Refresh() {
        base.Refresh();
        _lastUpTime = Time.time;
        isLevelComplete = false;
        _sec = -100;
        numText.gameObject.SetActive(false);
    }
}
