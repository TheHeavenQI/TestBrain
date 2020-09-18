using System.Collections.Generic;
using UnityEngine;
public class Level239 : LevelBasePage
{
    public List<RectTransform> blocks;
    public DragMove qiu;
    public Transform goal;
    private bool _showError;
    protected override void Start() {
        base.Start();
        qiu.onDrag = () =>
        {
            if (_showError)
            {
                return;
            }
            if(Vector3.Distance(qiu.transform.localPosition,goal.localPosition)< 50)
            {
                Completion();
                return;
            }
            var rect = qiu.transform.GetComponent<RectTransform>();
            for (int i = 0; i < blocks.Count; i++)
            {
                //blocks[i];
                if(RectTransformExtensions.IsRectTransformOverlap(blocks[i], rect)){
                    ShowError();
                    _showError = true;
                    After(() => {
                        Refresh();
                    }, 1);
                    return;
                }
            }
        };
    }
    public override void Refresh()
    {
        base.Refresh();
        _showError = false;
        qiu.Return2OriginPos();
    }

}
