using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Christ005 : LevelBasePage
{

    public List<Button> deffrients;

    public List<GameObject> diffrientsLable;

    private int total = 5;


    public void buttonEvent(int idx)
    {
        if (deffrients[idx * 2].enabled)
        {
            total -= 1;
            deffrients[idx * 2].enabled = false;
            deffrients[idx * 2 + 1].enabled = false;

            diffrientsLable[idx * 2].SetActive(true);
            diffrientsLable[idx * 2].GetComponentInChildren<Text>().text = "" + (5 - total);

            diffrientsLable[idx * 2 + 1].SetActive(true);
            diffrientsLable[idx * 2 + 1].GetComponentInChildren<Text>().text = "" + (5 - total);
        }
        
        if (total <= 0)
        {
            Completion();
        }
        deffrients[0].GetComponentInChildren<Text>().text = "" + total;

    }

    public override void Refresh()
    {
        base.Refresh();

        foreach (Button b in deffrients)
        {
            b.enabled = true;
        }

        foreach (GameObject obj in diffrientsLable)
        {
            obj.SetActive(false);
        }

        total = 5;

        deffrients[0].GetComponentInChildren<Text>().text = "" + total;
    }


}
