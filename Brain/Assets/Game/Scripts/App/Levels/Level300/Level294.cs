using UnityEngine;
public class Level294 : LevelBasePage
{
    public Transform axeTransform;
    public DragMove handle;
    private bool _finished;
    private Vector3 _axePos;
    private float _angle = 0;
    protected override void Start() {
        base.Start();
        _finished = false;
        _axePos = axeTransform.localPosition;
    }

    private void Update()
    {
        if (_finished)
        {
            return;
        }
        _angle += Time.deltaTime*100;
        axeTransform.localPosition -= new Vector3(0, Time.deltaTime * 80, 0);
        axeTransform.localEulerAngles = new Vector3(0, 0, _angle);
        if(axeTransform.localPosition.y < -650)
        {
            _finished = true;
            ShowError();
            After(() =>
            {
                Refresh();
            }, 1.5f);
        }
        if(Vector3.Distance(handle.transform.localPosition, axeTransform.localPosition) < 50)
        {
            _finished = true;
            int angle = (int)_angle % 360;
            if (angle > 90 && angle < 250)
            {
                ShowError();
                After(() =>
                {
                    Refresh();
                }, 1.5f);
            }
            else
            {
                handle.enabelDrag = false;
                Completion();
            }
        }
        
    }
    public override void Refresh()
    {
        base.Refresh();
        _finished = false;
        _angle = 0;
        handle.Return2OriginPos();
        axeTransform.localPosition = _axePos;
        axeTransform.localEulerAngles = Vector3.zero;
    }
}
