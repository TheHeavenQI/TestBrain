
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SelectNumLevel : LevelBasePage {
    public GameObject selectNumGameObject;
    public int answer;
    public Transform leftControl;
    Vector2 leftPos;
    public Transform rightControl;
    Vector2 rightPos;
    private Button _plus;
    private Button _reduce;
    private Button _okBtn;
    public Text _numText;
    public int _num;

    protected override void Awake() {
        base.Awake();
        if(leftControl != null)
            leftPos = leftControl.localPosition;
        if (rightControl != null)
            rightPos = rightControl.localPosition;
        _plus = selectNumGameObject.transform.Find("plus").GetComponent<Button>();
        _reduce = selectNumGameObject.transform.Find("reduce").GetComponent<Button>();
        var ok = selectNumGameObject.transform.Find("ok");
        _okBtn = selectNumGameObject.transform.Find("ok").GetComponent<Button>();
        _numText = selectNumGameObject.transform.Find("numbg/Text").GetComponent<Text>();
        
        _plus.onClick.AddListener(() => {
            _num++;
            _numText.text = $"{_num}";
        });
        _reduce.onClick.AddListener(() => {
            _num--;
            _num = Math.Max(_num, 0);
            _numText.text = $"{_num}";
        });
        _okBtn.onClick.AddListener(() => {
            if (_num == answer) {
                if(leftControl != null &&rightControl != null)
                    StartCoroutine("showSuc");
                else
                    Completion();
            }
            else {
                ShowError();
            }
        });
        Refresh();
    }

    public override void Refresh()
    {
        base.Refresh();
        StopCoroutine("showSuc");
        if (leftControl != null && rightControl != null)
        {
            leftControl.localPosition = leftPos;
            rightControl.localPosition = rightPos;
        }
        _num = 0;
        _numText.text = $"{_num}";
    }
    IEnumerator showSuc()
    {
        while (Vector3.Distance(leftControl.localPosition, rightPos) > 0.5f || Vector3.Distance(rightControl.localPosition, leftPos)>0.5f)
        {
            leftControl.localPosition = Vector3.Slerp(leftControl.localPosition,rightPos,0.03f);
            rightControl.localPosition = Vector3.Slerp(rightControl.localPosition,leftPos,0.03f);
            yield return null;
        }
        Completion();
    }
}
