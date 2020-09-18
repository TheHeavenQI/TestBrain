using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Level144 : LevelBasePage {

    public Button[] buttons;

    public DragMoveEventTrigger[] eventTriggers;

    private Vector3[] _originPos;
    private HashSet<DragMoveEventTrigger> _dragedSet = new HashSet<DragMoveEventTrigger>();
    private float _startDragTime;

    protected override void Start() {
        base.Start();

        foreach (Button button in buttons) {
            button.onClick.AddListener(() => {
                ShowErrorWithMousePosition();
            });
        }

        _originPos = eventTriggers.Select(it => it.transform.position).ToArray();

        for (int i = 0; i < eventTriggers.Length; ++i) {
            int k = i;
            eventTriggers[k].enableDragMove = false;
            eventTriggers[k].onDrag += (data) => OnDrag(eventTriggers[k], data);
            eventTriggers[k].onEndDrag += (data) => OnEndDrag(eventTriggers[k], data);
            eventTriggers[k].onBeginDrag += (data) => OnBeginDrag(eventTriggers[k], data);
        }
    }

    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < eventTriggers.Length; ++i) {
            eventTriggers[i].transform.position = _originPos[i];
            eventTriggers[i].enableDragMove = false;
        }
    }

    private void OnBeginDrag(DragMoveEventTrigger eventTrigger, PointerEventData data) {
        if (!_dragedSet.Contains(eventTrigger)) {
            _dragedSet.Add(eventTrigger);
        }
        if (_dragedSet.Count >= eventTriggers.Length) {
            _startDragTime = Time.time;
            foreach(DragMoveEventTrigger et in eventTriggers) {
                et.enableDragMove = true;
            }
        }
    }

    private void OnDrag(DragMoveEventTrigger eventTrigger, PointerEventData data) {
        if (_dragedSet.Count >= eventTriggers.Length) {
            if (Time.time - _startDragTime >= 0.5f) {
                Completion();
            }
        }
    }

    private void OnEndDrag(DragMoveEventTrigger eventTrigger, PointerEventData data) {
        if (_dragedSet.Contains(eventTrigger)) {
            _dragedSet.Remove(eventTrigger);
        }
        foreach (DragMoveEventTrigger et in eventTriggers) {
            et.enableDragMove = false;
        }
    }
}
