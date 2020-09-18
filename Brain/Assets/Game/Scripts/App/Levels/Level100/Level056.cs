using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level056 : LevelBasePage {
    public Button doorBtn;
    public GameObject openedDoor;

    public RectTransform[] trans;

    private Queue<float> _clickTimeQueue = new Queue<float>();
    private readonly int _needClickCount = 3;
    private readonly int _maxClickDeltaTime = 2;

    private Vector3[] _poss;

    protected override void Start() {
        base.Start();

        _poss = new Vector3[trans.Length];
        for (int i = 0; i < trans.Length; ++i) {
            _poss[i] = trans[i].position;
        }

        doorBtn.onClick.AddListener(() => {
            _clickTimeQueue.Enqueue(Time.time);
            while (Time.time - _clickTimeQueue.Peek() > _maxClickDeltaTime) {
                _clickTimeQueue.Dequeue();
            }
            if (_clickTimeQueue.Count >= _needClickCount) {
                openedDoor.SetActive(true);
                CompletionWithMousePosition();
            }
        });
    }

    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < trans.Length; ++i) {
            trans[i].position = _poss[i];
        }
    }
}
