using UnityEngine;
using UnityEngine.UI;

public class Level253 : LevelBasePage
{
    public GameObject mask;
    public Button btn;
    private bool _showBtn;
    protected override void Start() {
        base.Start();
        _showBtn = false;
        btn.gameObject.SetActive(false);
        btn.onClick.AddListener(() =>
        {
            Completion();
        });
    }
    private void Update()
    {
        ShowButton(NativeApi.GetBrightness() > 0.95f);
    }

    private void ShowButton(bool show)
    {
        if (_showBtn == show)
        {
            return;
        }
        _showBtn = show;
        btn.gameObject.SetActive(show);
    }
}
