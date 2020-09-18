
using UnityEngine;
using UnityEngine.UI;

public class Level262 : LevelBasePage
{
    public AnimalMove cat;
    public DragMoveEventTrigger fish;

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


        cat.jumpUpOffset = new Vector2(0, jumpSpeed);
        cat.jumpDownOffset = -cat.jumpUpOffset;

        fish.onEndDrag = (d) => { 
            if (RectTransformExtensions.IsRectTransformOverlap(cat.rectTransform, fish.rectTransform))
            {
                Completion();
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        cat.Refresh();
    }

    private void OnLeftClick()
    {
        cat.Moveto(new Vector2(-moveSpeed, 0));
    }

    private void OnRightClick()
    {
        cat.Moveto(new Vector2(moveSpeed, 0));
    }

    private void OnJumpClick()
    {
        cat.JumpUp();
    }
}
