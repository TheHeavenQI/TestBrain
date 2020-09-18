using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class Level090 : LevelBasePage
{

    public List<Image> images;
    private List<Vector3> positons = new List<Vector3>();

    private bool theKeyIsShow = false;

    public Sprite lightOn;
    public Sprite lightOff;

    public Image lightfront;

    protected override void Start()
    {
        base.Start();
        images[1].GetComponent<Canvas>().sortingLayerID = 0;
        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
            images[i].gameObject.GetComponent<DragMove>().onClick = () => {
                ShowErrorWithMousePosition();
            };
            images[i].gameObject.GetComponent<DragMove>().onDragEnd = OnDreagEnded;

            images[i].gameObject.GetComponent<DragMove>().enabelDrag = i == 1;

            if (i == 1)
            {
                images[i].gameObject.GetComponent<DragMove>().onDrag = OnDraging;
            }

        }

    }

    public override void Refresh()
    {
        base.Refresh();
        theKeyIsShow = false;
        images[1].transform.SetParent(images[0].transform);
        images[1].rectTransform.localScale = new Vector3(1, 1, 1);
        images[1].sprite = lightOn;
        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.localPosition = positons[i];
        }

        lightfront.enabled = false;
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
            images[1].transform.SetSiblingIndex(0);

            theKeyIsShow = true;
            images[1].sprite = lightOff;
            images[1].transform.DOScale(new Vector3(6, 6, 6), 0.5f);
        }
    }

    private void OnDreagEnded()
    {

        if (RectTransformExtensions.IsRectTransformOverlap(images[1].rectTransform.localPosition, images[1].rectTransform.sizeDelta * images[1].rectTransform.localScale, images[2].rectTransform.localPosition, images[2].rectTransform.sizeDelta * images[2].rectTransform.localScale) && theKeyIsShow)
        {
            images[1].transform.localPosition = images[2].rectTransform.localPosition + new Vector3(-11, 147, 0);
            //lightfront.GetComponent<Canvas>().sortingLayerID = images[1].GetComponent<Canvas>().sortingLayerID;
            lightfront.enabled = true;
        }

    }

    public void LightOnButtonEvent()
    {
        if (RectTransformExtensions.IsRectTransformOverlap(images[1].rectTransform.localPosition, images[1].rectTransform.sizeDelta * images[1].rectTransform.localScale, images[2].rectTransform.localPosition, images[2].rectTransform.sizeDelta * images[2].rectTransform.localScale) && theKeyIsShow)
        {
            Canvas ca = images[1].GetComponent<Canvas>();
            GraphicRaycaster sxzd = images[1].GetComponent<GraphicRaycaster>();
            Destroy(sxzd);
            Destroy(ca);

            Canvas ca1 = lightfront.GetComponent<Canvas>();
            GraphicRaycaster sxzd1 = lightfront.GetComponent<GraphicRaycaster>();
            Destroy(sxzd1);
            Destroy(ca1);

            // images[1].RecalculateClipping();
            images[1].sprite = lightOn;
            images[1].transform.SetParent(transform);
            images[1].transform.SetAsLastSibling();
            images[1].transform.localPosition = images[2].rectTransform.localPosition + new Vector3(-11, 147, 0);
            lightfront.transform.SetAsLastSibling();

            Completion();

        }
    }

}
