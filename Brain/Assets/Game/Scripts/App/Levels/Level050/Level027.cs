using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level027 : LevelBasePage {
    public List<Button> list;
    private GameObject _numFlagPrefab;
    private List<Button> clickBtn = new List<Button>();
    private List<GameObject> _nums = new List<GameObject>();
    protected override void Start() {
        base.Start();
        
        for (int i = 0; i < list.Count; i++) {
            var btn = list[i];
            var k = i;
            btn.onClick.AddListener(() => { Click(k); });
        }
        _numFlagPrefab = Resources.Load<GameObject>("Main/NumFlag");
    }

    private void Click(int index) {
        clickBtn.Add(list[index]);
        
        GameObject num = Instantiate(_numFlagPrefab);
        num.transform.SetParent(transform,false);
        var loc = LocFrom(list[index].transform);
        num.transform.localPosition = loc;
        num.GetComponent<Text>().text = $"{clickBtn.Count}";
        _nums.Add(num);
        
        
        if (clickBtn.Count == 3) {
            // 判断是否成功
            for (int i = 0; i < clickBtn.Count; i++) {
                if (clickBtn[i] != list[i]) {
                    ShowError();
                    After(() => {
                        Refresh();
                    },0.5f);
                    return;
                }
            }
            Completion();
        }
    }
    private Vector3 LocFrom(Transform transform) {
        Vector3 vector3 = transform.localPosition;
        var size = transform.GetComponent<RectTransform>().sizeDelta;
        var width = size.x;
        var height = size.y;
        var _index = clickBtn.Count;
        if (_index <= 3) {
            vector3.x -= width * 3 / 8;
        }
        else {
            vector3.x += width * 3 / 8;
        }

        if (_index == 1 || _index == 4) {
            vector3.y += height * 3 / 8;
        }else if (_index == 3 || _index == 6) {
            vector3.y -= height * 3 / 8;
        }
        return vector3;
    }
    

    public override void Refresh() {
        base.Refresh();
        clickBtn = new List<Button>();
        for (int i = 0; i < _nums.Count; i++) {
            Destroy(_nums[i]);
        }   
    }
}
