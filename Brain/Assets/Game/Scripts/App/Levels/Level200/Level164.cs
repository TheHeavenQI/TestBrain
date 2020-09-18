using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level164 : LevelBasePage
{
    public CustomEventTrigger bg;
    public List<DragMove> dragMoves;
    public Image ro;
    public Sprite beforeRoll;
    public Sprite afterRoll;

    private Vector2 _lastDelta = new Vector2(-1000, -1000);
    private Vector2 _lastPos = Vector2.zero;

    private float _angel = 0;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < dragMoves.Count; i++)
        {
            dragMoves[i].onClick = () => {
                ShowErrorWithMousePosition();
            };
        }
    }

    private void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
        {
            if (!bg.isPress)
            {
                return;
            }

            Vector2 touchDelta = Input.touchCount > 0 ? Input.GetTouch(0).deltaPosition : new Vector2(Input.mousePosition.x - _lastPos.x, Input.mousePosition.y - _lastPos.y);

            if (Vector2.Distance(touchDelta, _lastDelta) > 0.1)
            {
                if (Vector2.Distance(new Vector3(-1000, -1000), _lastDelta) < 0.1)
                {
                    _lastDelta = touchDelta;
                    _lastPos = Input.mousePosition;
                    return;
                }

                float a = Vector2.Angle(_lastDelta, touchDelta);
                _lastPos = Input.mousePosition;
                _lastDelta = touchDelta;
                _angel += a;
                ro.transform.localEulerAngles = new Vector3(ro.transform.localRotation.x, ro.transform.localRotation.y, _angel % 360);
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            if (_angel <= 10)
            {
                return;
            }

            if (_angel >= 360 * 3)
            {
                Completion();
            }
            else
            {
                ShowError();
            }
            Refresh();
        }
    }

    public override void Refresh()
    {
        base.Refresh();

        for (int i = 0; i < dragMoves.Count; i++)
        {
            dragMoves[i].Return2OriginPos();
        }
        _angel = 0;
        _lastDelta = new Vector3(-1000, -1000, -1000);
        ro.sprite = beforeRoll;
        ro.SetNativeSize();
        ro.transform.localEulerAngles = Vector3.zero;
    }

    protected override void OnCompletion()
    {
        base.OnCompletion();
        ro.sprite = afterRoll;
        ro.SetNativeSize();
    }
}
