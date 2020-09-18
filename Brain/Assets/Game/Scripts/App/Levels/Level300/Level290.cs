
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level290 : LevelBasePage
{
    public List<Sprite> starSprites;
    public Image ren;
    public Sprite renImage1;
    public Sprite renImage2;
    public GameObject zadan;
    public GameObject zadanOpen;
    public List<DragMove> drags;
    public List<Image> starImages;
    public List<Image> lineImages;
    private bool finished = false;
    private float _time;
    protected override void Start() {
        base.Start();
        _time = -0.01f;
        zadanOpen.SetActive(false);
        for(int i = 0; i < drags.Count; i++)
        {
            int k = i;
            drags[i].onDragEnd = () =>
            {
                if(Vector2.Distance(drags[k].transform.localPosition,ren.transform.localPosition)< 200)
                {
                    ren.sprite = renImage2;
                    ren.SetNativeSize();
                    ShowError();
                    finished = true;
                    drags[k].Return2OriginPos(0.5f);
                    After(() =>
                    {
                        Refresh();
                    },1);
                }
            };
        }
        TimeSec(0);
    }
    private void Update()
    {
        if (finished || isLevelComplete)
        {
            return;
        }
        int newTime = (int)(_time + Time.deltaTime);
        if ((int)(_time) != newTime)
        {
            TimeSec(newTime);
        }
        _time += Time.deltaTime;

        for(int i = 0; i < starImages.Count; i++)
        {
            int imageIndex = (int)(_time*10) % starImages.Count;
            starImages[i].sprite = starSprites[imageIndex];
        }

    }

    private void ShowLine(int line)
    {
        for (int i = 0; i < lineImages.Count; i++)
        {
            lineImages[i].gameObject.SetActive(i == line);
        }
    }
    private void TimeSec(int sec)
    {
        if(sec <= 1)
        {
            ShowLine(0);
        }
        else if(sec <= 3)
        {
            ShowLine(1);
        }
        else if (sec <= 5)
        {
            ShowLine(2);
        }

        if (sec == 6)
        {
            zadan.gameObject.SetActive(false);
            zadanOpen.gameObject.SetActive(true);
        }
        if(sec >= 7)
        {
            Completion();
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        ren.sprite = renImage1;
        ren.SetNativeSize();
        _time = -0.01f;
        finished = false;
    }
}
