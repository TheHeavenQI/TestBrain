using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class Level168 : LevelBasePage
{
    public Button successBtn;

    public List<Image> images;
    private List<Vector3> positons = new List<Vector3>();

    private bool theKeyIsShow = false;

    protected override void Start()
    {
        base.Start();
        levelQuestionText.rectTransform.sizeDelta = new Vector2(430, 570);


        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
            images[i].gameObject.GetComponent<DragMove>().onClick = () => {
                ShowErrorWithMousePosition();
            };
            images[i].gameObject.GetComponent<DragMove>().onDragEnd = ChangeShow;
            images[i].gameObject.GetComponent<DragMove>().enabelDrag = i == 1;

            if (i == 1)
            {
                images[i].gameObject.GetComponent<DragMove>().onDrag = OnDraging;
            }

        }

    }

    private void OnDraging()
    {

        if (theKeyIsShow)
        {
            return;
        }

        if (images[1].rectTransform.localPosition.y < -images[1].rectTransform.sizeDelta.y)
        {
            Vector3 posi = images[1].transform.position;
            images[1].transform.SetParent(images[0].transform.parent);
            images[1].transform.position = posi;

            theKeyIsShow = true;
        }
    }

    public override void Refresh()
    {
        base.Refresh();

        contentBGImg.GetComponent<Image>().color = Color.black;
        theKeyIsShow = false;

        images[1].transform.SetParent(images[0].transform);

        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.localPosition = positons[i];
        }

        successBtn.gameObject.SetActive(false);
    }

    public void ChangeShow()
    {
        if (!theKeyIsShow)
        {
            return;
        }
        contentBGImg.GetComponent<Image>().color = Color.white; 

        successBtn.gameObject.SetActive(true);
    }

    public void tapToShowSuccess()
    {
        Completion();
        images[1].GetComponent<Canvas>().overrideSorting = false;
        images[1].GetComponent<Canvas>().sortingOrder = 2;
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text.Replace("\\n", "\n");
    }
}
