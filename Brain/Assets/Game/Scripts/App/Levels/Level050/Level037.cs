using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level037 : LevelBasePage {
    public DragMove shitou;
    public RectTransform danRectTransform;
    public List<GameObject> maps;
    public Button mapBtn;
    private Vector3 _shitouPos;
    public List<Button> allButton;
    protected override void Start() {
        base.Start();
        _shitouPos = shitou.transform.localPosition;
        for (int i = 0; i < maps.Count; i++) {
            maps[i].SetActive(false);
        }
        shitou.onDragEnd = () => {
            if (RectTransformExtensions.IsRectTransformOverlap(shitou.transform.GetComponent<RectTransform>(),
                danRectTransform)) {
                danRectTransform.gameObject.SetActive(false);
                for (int i = 0; i < maps.Count; i++) {
                    maps[i].SetActive(true);
                }
            }
        };
        shitou.onClick = () => {
            ShowErrorWithMousePosition();
        };
        for (int i = 0; i < allButton.Count; i++) {
            var btn = allButton[i];
            btn.onClick.AddListener(() => {
                if (btn == mapBtn) {
                    CompletionWithMousePosition();
                }
                else {
                    ShowErrorWithMousePosition();
                }
            });
        }
    }

    public override void Refresh() {
        base.Refresh();
        shitou.transform.localPosition = _shitouPos;
        danRectTransform.gameObject.SetActive(true);
        for (int i = 0; i < maps.Count; i++) {
            maps[i].SetActive(false);
        }
    }
}
