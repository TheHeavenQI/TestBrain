using UnityEngine;

public class Level250 : LevelBasePage
{
    public DragMoveEventTrigger sun;
    public DragMoveEventTrigger moon;

    private readonly float _disSunMoon = 1160;
    private readonly float _moonBottom = -650;
    private readonly float _lightSumHeight = 480;
    private readonly float _darkSumHeight = -550;


    protected override void Start()
    {
        base.Start();

        sun.onDrag = (d) => {
            Vector3 pos = sun.transform.localPosition;

            float lightV = (pos.y - _darkSumHeight) / (_lightSumHeight - _darkSumHeight);
            lightV = Mathf.Clamp01(lightV);
            contentBGImg.color = Color.Lerp(new Color(0.3f, 0.3f, 0.3f, 1), Color.white, lightV);

            pos.y += _disSunMoon;
            moon.transform.localPosition = pos;
        };

        moon.onDrag = (d) => {
            Vector3 pos = moon.transform.localPosition;
            pos.y -= _disSunMoon;
            sun.transform.localPosition = pos;
        };

        moon.onEndDrag = (d) => {
            if (moon.transform.localPosition.y <= _moonBottom)
            {
                Completion();
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        sun.Return2OriginPos();
        moon.Return2OriginPos();
        contentBGImg.color = Color.white;
    }
}
