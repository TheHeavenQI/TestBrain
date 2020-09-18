
using UnityEngine;
using UnityEngine.UI;

public class Level280 : LevelBasePage
{
    public Image eyu;
    public Sprite openImage;
    public Sprite closeImage;
    public EventCallBack yachi;
    public DragMove gunzi;
    protected override void Start() {
        base.Start();
        yachi.onDragDraging = (pos) => {
            if (CheckGunziPos())
            {
                yachi.transform.position = pos;
            }
        };
        yachi.onDragBegin = () => {
            if (!CheckGunziPos())
            {
                eyu.sprite = closeImage;
                eyu.SetNativeSize();
                ShowError();
                After(() => {
                    Refresh();
                }, 2);
            }
        };
        yachi.onDragEnd = () => {
            if (CheckGunziPos())
            {
                Completion();
            }
        };
    }
    private bool CheckGunziPos()
    {
        if(Vector2.Distance(gunzi.transform.localPosition,new Vector3(-405, -351)) < 100){
            return true;
        }
        return false;
    }

    public override void Refresh()
    {
        base.Refresh();
        eyu.sprite = openImage;
        eyu.SetNativeSize();
        gunzi.Return2OriginPos();
    }

}
