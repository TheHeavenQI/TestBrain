using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level059 : LevelBasePage {
    public DragMove diannao;
    public GameObject offbtn;
    public GameObject onbtn;

    public GameObject offImage;

    protected override void Start() {
        base.Start();
        onbtn.GetComponent<Button>().onClick.AddListener(() => {
            onbtn.SetActive(false);
            offImage.SetActive(true);
            After(() => {
                Completion();
            },0.35f);
        });

        diannao.onClick = () =>
        {
            ShowErrorWithMousePosition();
        };

    }

    public override void Refresh() {
        base.Refresh();
        diannao.Return2OriginPos();
        onbtn.SetActive(true);
        offImage.SetActive(false);
    }
}
