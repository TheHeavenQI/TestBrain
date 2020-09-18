
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level173 : LevelBasePage {
    public RectTransform mOpSheep;
    public DragMove mOpSheepDrag;
    public GameObject mWolf;
    Sprite mNormalSheepSprite;
    public Sprite mSheepTriggerSprite;
    Image mSheep;
    
    public List<DragMove> dragList;
    RectTransform meat;
    protected override void Start() {
        base.Start();
        mSheep = mOpSheep.GetComponent<Image>();
        mNormalSheepSprite = mSheep.sprite;
        meat = dragList[2].rectTransform;
        mOpSheep.GetComponent<DragMove>().enabelDrag = false;
        dragList[2].onDrag = () =>
        {
            if (RectTransformExtensions.IsRectTransformOverlap(meat, mOpSheep))
            {
                mSheep.sprite = mSheepTriggerSprite;
                mOpSheepDrag.enabelDrag = true;
            }
        };
        foreach (var item in dragList)
            item.onDragEnd = () => { item.Return2OriginPos(); };
        mOpSheepDrag.onDragBegin = () =>
        {
            if (!mOpSheepDrag.enabelDrag)
                return;
            mWolf.SetActive(true);
        };
    }
   
    public override void Refresh() {
        base.Refresh();
        foreach (var item in dragList)
            item.Return2OriginPos();
        mOpSheepDrag.Return2OriginPos();
        mOpSheepDrag.enabelDrag = false;
        mWolf.SetActive(false);
        mSheep.sprite = mNormalSheepSprite;
    }
    public void ClickWolf()
    {
        Completion();
    }
}
