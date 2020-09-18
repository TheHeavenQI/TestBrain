using UnityEngine;
using DG.Tweening;

public class Level254 : LevelBasePage
{
    public GameObject bomb;
    public GameObject bombExplode;
    public CustomEventTrigger[] eventTriggers;

    protected override void Start()
    {
        base.Start();

        bomb.SetActive(true);
        bombExplode.SetActive(false);

        for (int i = 0; i < eventTriggers.Length; ++i)
        {
            int j = i;
            eventTriggers[j].onPointerDown = (d) => {

                if (j == 1 || j == 2)
                {
                    ShowError();
                    Explode();
                    After(Refresh, 1);
                }

                if (j == 0 && eventTriggers[3].isPress
                    || j == 3 && eventTriggers[0].isPress)
                {
                    Completion();
                }
            };
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        bomb.SetActive(true);
        bombExplode.SetActive(false);
    }

    private void Explode()
    {
        bomb.SetActive(false);
        bombExplode.SetActive(true);
        bombExplode.transform.DOScale(0.2f, 0.5f).From();
    }
}
