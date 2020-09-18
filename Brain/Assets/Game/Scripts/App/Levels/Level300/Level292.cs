
using UnityEngine;
using UnityEngine.UI;

public class Level292 : LevelBasePage
{
    public Button btn;
    public ScrollRect scrollRect;
    protected override void Start() {
        base.Start();
        btn.onClick.AddListener(() =>
        {
            Completion();
        });
        scrollRect.content.anchoredPosition = new Vector3(0, 97950, 0);
    }
    public override void Refresh()
    {
        base.Refresh();
        scrollRect.content.anchoredPosition = new Vector3(0, 97950, 0);
    }
}
