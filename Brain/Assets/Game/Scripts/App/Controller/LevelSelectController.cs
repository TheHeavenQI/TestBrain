
using SuperScrollView;
using UnityEngine.UI;

public class LevelSelectController : BaseController
{
    public LoopListView2 mLoopListView;
    const int mItemCountPerRow = 2;// how many items in one row
    private int totalCount = 0;
    public Button header;
    public void Start() {
        base.Start();
        totalCount = ConfigManager.Current().Questions.Count+1;
        int count = totalCount / mItemCountPerRow;
        if (totalCount % mItemCountPerRow > 0)
        {
            count++;
        }
        //count is the total row count
        mLoopListView.InitListView(count+1, OnGetItemByIndex);
        mLoopListView.ResetListView();
        header.onClick.AddListener(() => {
            ControllerManager.Instance.GetController<LevelSelectChristmasController>().gameObject.SetActive(true);
        });
        var model = UserModel.Get();
        int index = model.levelMaxId/mItemCountPerRow;
        mLoopListView.MovePanelToItemIndex(index,500);
    }
    
    private void OnEnable() {
        base.OnEnable();
        ADManager.CloseAD(GameAdID.Banner);
        mLoopListView.RefreshAllShownItem();
    }

    public override void Hide() {
        base.Hide();
        ContentController.Instance.currentLevelPage.RefreshBanner();
    }
    
    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
    {
        if (rowIndex < 0)
        {
            return null;
        }

        if (rowIndex == 0) {
            LoopListViewItem2 EmptyImage = listView.NewListViewItem("SelectHeaderView");
            return EmptyImage;
        }
        
        var row = rowIndex - 1;
        //create one row
        LoopListViewItem2 item = listView.NewListViewItem("RowPrefab");
        LevelRowItem itemScript = item.GetComponent<LevelRowItem>();
        itemScript.SetType(1);
        //update all items in the row
        for (int i = 0; i < mItemCountPerRow; ++i)
        {
            int itemIndex = row * mItemCountPerRow + i;
            itemScript.mItemList[i].SetType(1);
            if (itemIndex >= totalCount)
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
        if (index >= totalCount - 1) {
            PopUpManager.Instance.ShowAllFinish();
            return;
        }
        Global.christmas = false;
        var a = ControllerManager.Instance.GetController<ContentController>();
        a.levelIndex = index+1;
        a.ShowLevel();
        Hide();
        
    }

}
