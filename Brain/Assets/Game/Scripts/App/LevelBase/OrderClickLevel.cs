using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderClickLevel : LevelBasePage {
    public List<Button> list;
    public int maxClickCount;
    private List<Attr> _flagList = new List<Attr>();
    private List<GameObject> _nums = new List<GameObject>();
    private GameObject _numFlagPrefab;
    public List<int> canRepeatNumber { set; get; } = new List<int>();

    /// <summary>
    /// 点击的数字
    /// </summary>
    private List<int> _clickNum = new List<int>();
    protected override void Awake() {
        base.Awake();
        canRepeatNumber.Add(0);
        for (int i = 0; i < list.Count; i++) {
            var attr = list[i].GetComponent<Attr>();
            _flagList.Add(attr);
            Debug.Log($"aa {_flagList[i]}");
        }
        _numFlagPrefab = Resources.Load<GameObject>("Main/NumFlag");
    }

    protected override void Start() {
        base.Start();
        for (int i = 0; i < list.Count; i++) {
            var btn = list[i];
            var k = i;
            btn.onClick.AddListener(() => { Click(k); });
        }
        Refresh();
    }

    public override void Refresh()
    {
        base.Refresh();
        _clickNum = new List<int>();
        for (int i = 0; i < _nums.Count; i++)
        {
            Destroy(_nums[i]);
        }
    }

    private Vector3 LocFrom(Transform transform) {
        Vector3 vector3 = transform.localPosition;
        var size = transform.GetComponent<RectTransform>().sizeDelta;
        var width = size.x;
        var height = size.y;
        var _index = _clickNum.Count;
        if (_index <= 3) {
            vector3.x -= width * 3 / 8;
        } else {
            vector3.x += width * 3 / 8;
        }

        if (_index == 1 || _index == 4) {
            vector3.y += height * 3 / 8;
        } else if (_index == 3 || _index == 6) {
            vector3.y -= height * 3 / 8;
        }
        return vector3;
    }
    private void Click(int index)
    {
        var attr = _flagList[index];
        _clickNum.Add(attr.flag);
        GameObject num = Instantiate(_numFlagPrefab);
        num.transform.SetParent(transform, false);
        var loc = LocFrom(list[index].transform);
        num.transform.localPosition = loc;
        num.GetComponent<Text>().text = $"{_clickNum.Count}";
        _nums.Add(num);

        if (_clickNum.Count == maxClickCount)
        {
            bool error = false;
            for (int i = 0; i < _clickNum.Count - 1; i++)
            {
                bool isRepeatNum = false;
                if (_clickNum[i] == _clickNum[i + 1])
                {
                    for (int j = 0; j < canRepeatNumber.Count; j++)
                    {
                        if (_clickNum[i] == canRepeatNumber[j])
                        {
                            isRepeatNum = true;
                            break; ;
                        }
                    }
                }
                if (_clickNum[i] > _clickNum[i + 1] || (!isRepeatNum && _clickNum[i] == _clickNum[i + 1]))
                {
                    error = true;
                    Debug.Log($"{_clickNum[i]}   {_clickNum[i + 1]} {i}");
                    break;
                }
            }

            if (error ||containsError(_clickNum))
            {
                ShowError();
                Refresh();
            }
            else
            {
                Completion();
            }
        }
    }
    protected bool containsError(List<int> sel)
    {

        foreach (var item in _flagList)
        {
            if (sel.Contains(item.flag) && !item.isInCorrectAsw)
                return true;
        }
        return false;

    }
}
