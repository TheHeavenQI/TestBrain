using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level065 : SelectNumLevel
{
    public override void Refresh()
    {
        base.Refresh();
        _num = 4;
        _numText.text = $"{_num}";
    }
}
