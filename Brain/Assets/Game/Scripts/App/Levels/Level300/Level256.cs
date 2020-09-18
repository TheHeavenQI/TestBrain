using UnityEngine;
using UnityEngine.UI;
public class Level256 : LevelBasePage
{

    public Level256LuckySpin luckySpin;
    public Button startBtn;

    protected override void Start()
    {
        base.Start();

        luckySpin.onFinish = OnTrunFinish;
        luckySpin.GetComponent<Button>().onClick.AddListener(() => {
            if (!luckySpin.IsStoped())
            {
                luckySpin.ForceStop();
                float angle = luckySpin.ClampAngle(luckySpin.transform.localEulerAngles.z);
                if (angle >= 266 && angle <= 330)
                {
                    Completion();
                }
                else
                {
                    ShowError();
                }
            }
        });

        startBtn.onClick.AddListener(() => {
            luckySpin.StartTurn(1);
        });
    }

    private void OnTrunFinish(DeviceOrientation orientation)
    {
        ShowError();
    }
}
