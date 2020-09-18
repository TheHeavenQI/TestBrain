using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level067 : LevelBasePage {
    public List<Text> timeList;
    public List<Button> numList;
    private List<int> nums = new List<int>();
    public Button okBtn;
    protected override void Start() {
        base.Start();
        okBtn.GetComponent<Button>().onClick.AddListener(() => { CheckFinish(); });
        for (int i = 0; i < numList.Count; i++) {
            var btn = numList[i];
            var num = int.Parse(btn.name);
            btn.onClick.AddListener(() => {
                nums.Add(num);
                RefreshNum();
            });
        }
    }
    
    private void RefreshNum() {
        for (int i = 0; i < nums.Count && i < timeList.Count; i++) {
            timeList[i].text = $"{nums[i]}";
        }
    }
    private void CheckFinish() {
        if (nums.Count >= 4) {
            var a = DateTime.Now.ToLocalTime();
            var hour = nums[0] * 10 + nums[1];
            var minute = nums[2] * 10 + nums[3];
            if (hour == a.Hour && Math.Abs(a.Minute - minute)<= 1 ) {
                Completion();
                return;
            }
        }
        ShowError();
        Refresh();
    }
    public override void Refresh() {
        base.Refresh();
        nums = new List<int>();
        for (int i = 0; i < timeList.Count; i++) {
            timeList[i].text = null;
        }
    }
}
