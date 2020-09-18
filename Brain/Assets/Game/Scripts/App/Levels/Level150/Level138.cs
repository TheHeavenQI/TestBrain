using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level138 : SumClickLevel {

    public Button candyMaskBtn1;
    public Button candyMaskBtn2;
    public Button resetBtn;
    public DragMove commitDM;
    public GameObject left;
    public GameObject right;
    protected override void Start() {
        base.Start();

        candyMaskBtn1.onClick.AddListener(() => {
            btnList[0].gameObject.SetActive(true);
            candyMaskBtn1.gameObject.SetActive(false);
            left.SetActive(true);
        });

        candyMaskBtn2.onClick.AddListener(() => {
            btnList[1].gameObject.SetActive(true);
            candyMaskBtn2.gameObject.SetActive(false);
            right.SetActive(true);
        });
        left.SetActive(false);
        right.SetActive(false);
        resetBtn.onClick.AddListener(Refresh);
    }

    public override void Refresh() {
        base.Refresh();

        btnList[0].gameObject.SetActive(false);
        btnList[1].gameObject.SetActive(false);

        candyMaskBtn1.gameObject.SetActive(true);
        candyMaskBtn2.gameObject.SetActive(true);
        left.SetActive(false);
        right.SetActive(false);
        commitDM.Return2OriginPos();
    }
}
