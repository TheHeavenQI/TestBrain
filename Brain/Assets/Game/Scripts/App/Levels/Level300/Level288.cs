
using System.Collections.Generic;
using UnityEngine.UI;

public class Level288 : LevelBasePage
{
    public List<EventCallBack> btns;
    protected override void Start() {
        base.Start();
        for(int i = 0 ;i< btns.Count; i++)
        {
            btns[i].needPressTime = 1;
            btns[i].onLongPress = () =>
            {
                Completion();
            };
        }
    }
    
}
