
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level274 : LevelBasePage
{
    public ScrollRect scrollRect;
    public Scrollbar scrollbar;
    public List<DragMove> dragMovesList;
    public Button blueButton;

    private int bookWidth = 619;
    private int _currentOffset;
    protected override void Start() {
        base.Start();
        _currentOffset = -1;
        SetDragMoveRaycastTarget(false);
        blueButton.onClick.AddListener(()=>
        {
            Completion();
        });
        scrollbar.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(0);
        for (int i = 0; i < dragMovesList.Count; i++)
        {
            dragMovesList[i].onDragEnd = () =>
            {
                blueButton.enabled = true;
            };
        }
    }
    private void OnValueChanged(float value)
    {
        int offset = -(int)(scrollRect.content.transform.localPosition.x / bookWidth);
        if(_currentOffset == offset)
        {
            return;
        }
        blueButton.enabled = false;
        UtilsLog.Log($"scrollRect:{scrollRect.content.transform.localPosition}");
        _currentOffset = offset;
        for(int i = 0;i< dragMovesList.Count; i++)
        {
            Vector3 a = dragMovesList[i].rectTransform.anchoredPosition;
            a.x = bookWidth * (_currentOffset + i - 1);
            a.y = 0;
            dragMovesList[i].rectTransform.anchoredPosition = a;
        }
    }
    
    private void SetDragMoveRaycastTarget(bool raycastTarget)
    {
        for (int i = 0; i < dragMovesList.Count; i++)
        {
            dragMovesList[i].GetComponent<Image>().raycastTarget = raycastTarget;
        }
    }

    private void Update()
    {
        SetDragMoveRaycastTarget(Input.touchCount >= 1);
        scrollRect.horizontal = Input.touchCount < 2;
    }

    public override void Refresh()
    {
        base.Refresh();
        _currentOffset = -1;
        OnValueChanged(0);
    }
}
