
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level210 : LevelBasePage
{
    public AnimalMove cat1;
    public AnimalMove cat2;

    public Button leftBtn;
    public Button rightBtn;
    public Button jumpBtn;

    private float moveSpeed = 50;
    private float jumpSpeed = 200;

    protected override void Start()
    {
        base.Start();

        leftBtn.onClick.AddListener(OnLeftClick);
        rightBtn.onClick.AddListener(OnRightClick);
        jumpBtn.onClick.AddListener(OnJumpClick);


        cat1.jumpUpOffset = new Vector2(0, jumpSpeed);
        cat1.jumpDownOffset = -cat1.jumpUpOffset;

        cat2.jumpUpOffset = cat1.jumpDownOffset;
        cat2.jumpDownOffset = cat1.jumpUpOffset;

        cat2.correctAction = () => Completion();

        Vector3 pos = cat2.transform.localPosition;
        pos.x = 750 - cat1.transform.localPosition.x;
        cat2.transform.localPosition = pos;
    }

    public override void Refresh()
    {
        base.Refresh();
        cat1.Refresh();
        cat2.Refresh();
    }

    private void OnLeftClick()
    {
        cat1.Moveto(new Vector2(-moveSpeed, 0));
        cat2.Moveto(new Vector2(moveSpeed, 0));
    }

    private void OnRightClick()
    {
        cat1.Moveto(new Vector2(moveSpeed, 0));
        cat2.Moveto(new Vector2(-moveSpeed, 0));
    }

    private void OnJumpClick()
    {
        cat1.JumpUp();
        cat2.JumpUp();
    }
}
