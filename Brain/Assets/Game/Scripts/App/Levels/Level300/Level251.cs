using UnityEngine;

public class Level251 : LevelBasePage
{
    public GameObject sun;
    public DragMove moon;
    public GameObject mask;
    public GameObject gui;

    private float _winMoonY = -600;

    protected override void Start()
    {
        base.Start();
        sun.SetActive(false);

        moon.onDragEnd = () => {
            if (moon.transform.localPosition.y < _winMoonY)
            {
                sun.SetActive(true);
                gui.SetActive(false);
                moon.gameObject.SetActive(false);
                Completion();
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        sun.SetActive(false);
        moon.Return2OriginPos();
        moon.gameObject.SetActive(true);
        gui.SetActive(true);
    }
}
