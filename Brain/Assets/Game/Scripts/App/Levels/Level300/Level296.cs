using UnityEngine;
using UnityEngine.UI;
using BaseFramework;

public class Level296 : LevelBasePage
{
    public Level296Hole[] holes;

    private int _clickCount = 0;
    private bool _isReadyCompletion = false;
    private bool _isDead = false;

    protected override void Start()
    {
        base.Start();
        foreach (var hole in holes)
        {
            hole.onClick += (h) => {
                if (hole.showCount == 0 && !_isDead)
                {
                    if (_clickCount < 3)
                    {
                        h.ShowGameObject(Level296Hole.GOType.rabbit);
                    }
                    else
                    {
                        _isDead = true;
                        h.ShowGameObject(Level296Hole.GOType.snake);
                        ShowError(h.transform.localPosition);
                        After(Refresh, 1);
                    }
                    ++_clickCount;
                }
            };
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        foreach (var hole in holes)
        {
            hole.Clear();
        }

        _clickCount = 0;
        _isReadyCompletion = false;
        _isDead = false;
    }

    private void Update()
    {
        if (isLevelComplete)
        {
            return;
        }

        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown || Input.GetKey(KeyCode.UpArrow))
        {
            if (_isReadyCompletion)
            {
                return;
            }
            _isReadyCompletion = true;
            holes.GetRandomItem(it => it.showCount == 0).ShowGameObject(Level296Hole.GOType.esterEgg);
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.GetKey(KeyCode.DownArrow))
        {
            if (_isReadyCompletion)
            {
                Completion();
            }
        }
    }
}
