
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level286 : LevelBasePage
{
    public Sprite image1;
    public Sprite image2;
    public Button btn;
    public GameObject fire;
    public DragMove huocai;
    public List<DragMove> drags;
    private List<Image> dragsImage = new List<Image>();
    private List<int> countList = new List<int>();
    private Vector2 vector1 = new Vector2(-140, -280);
    private Vector2 vector2 = new Vector2(140, -200);
    protected override void Start() {
        base.Start();
        for(int i = 0;i < drags.Count; i++)
        {
            int k = i;
            dragsImage.Add(drags[i].GetComponent<Image>());
            countList.Add(0);
            drags[i].onDrag = () =>
            {
                JudgeImage(k);
            };
        }
        fire.SetActive(false);
        btn.onClick.AddListener(() =>
        {
            fire.SetActive(true);
            After(() =>
            {
               Completion();
            },1);
        });
    }
    public override void Refresh()
    {
        base.Refresh();
        fire.SetActive(false);
        for(int i =0;i< dragsImage.Count; i++)
        {
            dragsImage[i].sprite = image1;
            drags[i].Return2OriginPos();
            countList[i] = 0;
        }
        huocai.Return2OriginPos();
    }

    private void JudgeImage(int index)
    {
        Image image = dragsImage[index];
        Vector2 pos = image.transform.localPosition;
        if (pos.x > vector1.x && pos.x < vector2.x && pos.y > vector1.y && pos.y < vector2.y)
        {
            countList[index] += 1;
            if(countList[index] > 100)
            {
                image.sprite = image2;
            }
        }
    }
}
