
using UnityEngine.UI;

public class Level131 : LevelBasePage {
    public Button blue;
    public Button red;
    public Text numText;
    private int _count = 0;
    protected override void Start() {
        base.Start();
        blue.onClick.AddListener(() => {
            if (_count == 6) {
                _count += 2;
            }
            else {
                _count += 1;
            }
            numText.text = $"{_count}";
        });
        red.onClick.AddListener(() => {
            if (_count == 11) {
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
        _count = 0;
        numText.text = "";
    }
}
