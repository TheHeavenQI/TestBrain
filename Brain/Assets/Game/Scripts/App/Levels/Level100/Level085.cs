
using System.Collections.Generic;
using UnityEngine.UI;

public class Level085 : LevelBasePage {
    public List<Button> btnlist;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < btnlist.Count; i++) {
            var btn = btnlist[i];
            btn.onClick.AddListener(()=>
            {
                if (btn.name == "White") {
                    Completion();
                }
                else {
                    ShowErrorWithMousePosition();
                }
            });
        }
    }
    
}
