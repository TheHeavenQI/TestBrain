using UnityEngine;
using UnityEngine.UI;

public class Level268 : LevelBasePage
{
    public Button[] btns;
    private GameObject _numFlagPrefab;

    private GameObject _numFlag;

    private int _lastClickBtn = -1;

    protected override void Start()
    {
        base.Start();
        _numFlagPrefab = Resources.Load<GameObject>("Main/NumFlag");

        for (int i = 0; i < btns.Length; ++i)
        {
            int j = i;
            btns[j].onClick.AddListener(() => {
                if (_lastClickBtn < 0)
                {
                    _lastClickBtn = j;
                    ShowNumFlag(j);
                }
                else if (_lastClickBtn == j)
                {
                    Completion();
                }
                else
                {
                    ShowError();
                    Refresh();
                }
            });
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        if (_numFlag != null)
        {
            Destroy(_numFlag);
            _numFlag = null;
        }
        _lastClickBtn = -1;
    }

    private void ShowNumFlag(int index)
    {
        _numFlag = Instantiate(_numFlagPrefab);
        _numFlag.transform.SetParent(btns[index].transform.parent, false);
        Vector3 loc = LocFrom(btns[index].image.rectTransform);
        _numFlag.transform.localPosition = loc;
        _numFlag.GetComponent<Text>().text = $"{1}";
    }

    private Vector3 LocFrom(RectTransform rectTransform)
    {
        Vector3 aimPos = rectTransform.localPosition;
        var size = rectTransform.sizeDelta;
        aimPos.y -= size.y * 0.3f;
        aimPos.x += size.x * 0.1f;
        return aimPos;
    }
}
