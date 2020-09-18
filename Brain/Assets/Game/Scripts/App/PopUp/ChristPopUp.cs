using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChristPopUp : BasePopUp
{
    public Button btn;
    public override void Start() {
        base.Start();
        btn.onClick.AddListener(() => {
            ControllerManager.Instance.GetController<LevelSelectChristmasController>().gameObject.SetActive(true);
            Hide();
        });
    }
}
