using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level187 : LevelBasePage
{
    public Transform canSelRoot;
    private List<int> nums = new List<int>();
    public RectTransform mCanRotateNum;

    public RectTransform[] mTargetRect;
    Dictionary<Transform, Vector3> difPos = new Dictionary<Transform, Vector3>();
    protected override void Start()
    {
        base.Start();
        foreach (Transform child in canSelRoot)
        {
            difPos.Add(child,child.localPosition);
        }
    }
    public void ClickItem(RectTransform numRect)
    {
        string _name = numRect.name;
        int index = int.Parse(_name.Split('_')[1]);
        int useNum = index;
        if (index == 9)
        {
            float z = mCanRotateNum.localEulerAngles.z;
            float z_1 = z - 180; float z_2 = z + 180;
            if (Mathf.Abs(z_1) < 30 || Mathf.Abs(z_2) < 30)
            {
                useNum = 6;
            }
        }
        if (nums.Count > 2)
            return;
        RectTransform _target = mTargetRect[nums.Count];
        numRect.SetParent(_target);

        RotateNum script = numRect.GetComponent<RotateNum>();
        if (script != null)
            script.enableRotate = false;
        numRect.GetComponent<Button>().enabled = false;
        nums.Add(useNum);
        Tweener t = numRect.DOLocalMove(Vector3.zero, 0.2f);
        t.onComplete = () =>
        {
            if (nums.Count == 3)
            {
                int all = nums[0] + nums[1] + nums[2];
                if (all == 30)
                    Completion();
                else
                {
                    ShowError();
                    Refresh();
                }
            }
        };
    }
    public override void Refresh()
    {
        base.Refresh();
        mCanRotateNum.localEulerAngles = Vector3.zero;
        foreach (var item in difPos)
        {
            item.Key.SetParent(canSelRoot);
            item.Key.GetComponent<Button>().enabled = true;
            RotateNum script = item.Key.GetComponent<RotateNum>();
            if (script != null)
                script.enableRotate = true;
            item.Key.localPosition = item.Value;
        }
        nums.Clear();
    }

}
