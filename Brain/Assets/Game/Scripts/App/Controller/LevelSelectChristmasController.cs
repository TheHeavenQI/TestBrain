using System;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LevelSelectChristmasController : BaseController
{
    public LoopListView2 mLoopListView;
    const int mItemCountPerRow = 2;// how many items in one row

    private int total_count;
    public void Start() {
        base.Start();
        total_count = ConfigManager.Current().Activities.christ.Count;
        int count = total_count / mItemCountPerRow;
        if (total_count % mItemCountPerRow > 0)
        {
            count++;
        }
        //count is the total row count
        mLoopListView.InitListView(count+1, OnGetItemByIndex);
        mLoopListView.ResetListView();
    }
    
    private void OnDisable() {
        MusicManager.Instance.PlayBgMusic();
    }

    private void OnEnable() {
        base.OnEnable();
        MusicManager.Instance.PlayChristmasMusic();
        ADManager.CloseAD(GameAdID.Banner);
        mLoopListView.RefreshAllShownItem();
    }
    
    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
    {
        if (rowIndex < 0)
        {
            return null;
        }

        if (rowIndex == 0) {
            LoopListViewItem2 EmptyImage = listView.NewListViewItem("SelectHeaderChristmasView");
            EmptyImage.transform.SetAsFirstSibling();
            return EmptyImage;
        }
        
        var row = rowIndex - 1;
        //create one row
        LoopListViewItem2 item = listView.NewListViewItem("RowPrefab");
        LevelRowItem itemScript = item.GetComponent<LevelRowItem>();
        itemScript.SetType(2);
        //update all items in the row
        for (int i = 0; i < mItemCountPerRow; ++i)
        {
            int itemIndex = row * mItemCountPerRow + i;
            itemScript.mItemList[i].SetType(2);
            if (itemIndex >= total_count)
            {
                itemScript.mItemList[i].gameObject.SetActive(false);
                continue;
            }
            //update the subitem content.
            itemScript.mItemList[i].gameObject.SetActive(true);
            itemScript.mItemList[i].SetIndex(itemIndex);
            itemScript.mItemList[i].clickAction = ItemClick;
        }
        return item;
    }
    
    private void ItemClick(int index) {
        Global.christmas = true;
        var a = ControllerManager.Instance.GetController<ContentController>();
        a.levelIndex = index+1;
        a.ShowLevel();
        PlayerPrefs.SetInt(Constance.storage_enterChrist,1);
        Hide();
        ControllerManager.Instance.GetController<LevelSelectController>().gameObject.SetActive(false);
    }

}
