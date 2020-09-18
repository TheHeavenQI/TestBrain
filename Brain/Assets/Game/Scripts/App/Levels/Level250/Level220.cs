using UnityEngine;
using UnityEngine.UI;

public class Level220 : LevelBasePage
{
    public Level220LuckySpin luckySpin;
    public Button startBtn;

    protected override void Start()
    {
        base.Start();

        luckySpin.onFinish = OnTrunFinish;

        startBtn.onClick.AddListener(() => {
            luckySpin.StartTurn(1);
        });
    }

    private void OnTrunFinish(DeviceOrientation orientation)
    {
        if (orientation == DeviceOrientation.PortraitUpsideDown)
        {
            Completion();
        }
        else
        {
            ShowError();
        }
    }
}
