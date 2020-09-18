
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level278 : LevelBasePage
{
    public DragMove fingerDragMove;
   
    public List<Transform> list;
    public Sprite image1;
    public Sprite image2;
    private List<Vector3> _listPos = new List<Vector3>();
    public Button btn;
    protected override void Start() {
        base.Start();
        for(int i = 0;i < list.Count; i++)
        {
            _listPos.Add(list[i].localPosition);
        }
        fingerDragMove.onDrag = () =>
        {
            var pos = fingerDragMove.transform.localPosition;
            if(pos.y < list[0].localPosition.y && pos.y > list[3].localPosition.y && pos.x > list[0].localPosition.x)
            {
                fingerDragMove.enabelDrag = false;
                fingerDragMove.GetComponent<Image>().sprite = image2;
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].localPosition += new Vector3(0, i<3?-30:30, 0);
                }
                ShowError();
                After(() =>
                {
                    Refresh();
                }, 1);
            }
        };
        btn.onClick.AddListener(() =>
        {
            Completion();
        });
    }
    public override void Refresh()
    {
        base.Refresh();
        fingerDragMove.Return2OriginPos();
        fingerDragMove.enabelDrag = true;
        fingerDragMove.GetComponent<Image>().sprite = image1;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].localPosition = _listPos[i];
        }
    }

}
