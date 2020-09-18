using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SumClickLevel : LevelBasePage {

    public List<Button> btnList;
    /// <summary>
    /// 若点击次数小于等于0，则不检测点击次数
    /// </summary>
    public int maxClickCount = -1;
    public int tragetSum;
    public bool isAllowRepeat = false;

    private List<Attr> _attrList = new List<Attr>();
    /// <summary>
    /// 被选中的Attr
    /// </summary>
    private List<Attr> _clickedAttrList = new List<Attr>();

    private GameObject _numFlagPrefab;
    /// <summary>
    /// 点击后创建的数值
    /// </summary>
    private List<GameObject> _nums = new List<GameObject>();

    protected override void Awake() {
        base.Awake();
        for (int i = 0; i < btnList.Count; i++) {
            var attr = btnList[i].GetComponent<Attr>();
            _attrList.Add(attr);
            Debug.Log($"aa {_attrList[i]}");
        }
        _numFlagPrefab = Resources.Load<GameObject>("Main/NumFlag");
    }

    protected override void Start() {
        base.Start();
        for (int i = 0; i < btnList.Count; i++) {
            var btn = btnList[i];
            var k = i;
            btn.onClick.AddListener(() => { Click(k); });
        }
    }

    public override void Refresh() {
        base.Refresh();
        _clickedAttrList.Clear();
        for (int i = 0; i < _nums.Count; i++) {
            Destroy(_nums[i]);
        }
    }

    protected Vector3 LocFrom(Transform transform) {
        Vector3 vector3 = transform.localPosition;
        var size = transform.GetComponent<RectTransform>().sizeDelta;
        var width = size.x;
        var height = size.y;
        var _index = _clickedAttrList.Count;
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
    protected void Click(int index) {

        Attr attr = _attrList[index];
        if (!isAllowRepeat && _clickedAttrList.Contains(attr)) {
            return;
        }

        _clickedAttrList.Add(attr);
        GameObject num = Instantiate(_numFlagPrefab);
        num.transform.SetParent(transform, false);
        var loc = LocFrom(btnList[index].transform);
        num.transform.localPosition = loc;
        num.GetComponent<Text>().text = $"{_clickedAttrList.Count}";
        _nums.Add(num);

        int sum = _clickedAttrList.Sum((it) => it.flag);

        if (_clickedAttrList.Count == maxClickCount) {
            if (sum == tragetSum) {
                Completion();
            } else {
                ShowError();
                Refresh();
            }
        } else if (maxClickCount <= 0) {
            if (sum == tragetSum) {
                Completion();
            } else if (sum > tragetSum) {
                ShowError();
                Refresh();
            }
        }
    }
}
